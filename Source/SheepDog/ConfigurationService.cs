#region MIT License
/*
MIT License
Copyright (c) 2009 Joshua Sklare
http://www.codeplex.com/SheepDog

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using SheepDog.Properties;

namespace SheepDog
{
    /// <summary>
    /// Service which controls saving, loading, and access to the application's
    /// configuration settings.
    /// </summary>
    public class ConfigurationService : BaseControl
    {
        private EventHandler _savedEvent;
        private Icon _defaultTrayIcon;

        /// <summary>
        /// Creates a new instance of the <see cref="ConfigurationService"/> class.
        /// </summary>
        public ConfigurationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {}

        /// <summary>
        /// Initializes the configuration service by loading the configuration.
        /// </summary>
        public void Initialize()
        {
            _defaultTrayIcon = new Icon(Resources.DefaultTrayIcon, 16, 16);
            Hotkey = new Hotkey {Windows = true, Key = Keys.O};
            LoadDefaultSettings();

            Load();
        }

        /// <summary>
        /// 
        /// </summary>
        public Hotkey Hotkey
        {
            get; set;
        }

        /// <summary>
        /// Indicates whether a global hotkey is enabled for this application.
        /// </summary>
        public Boolean HotkeyEnabled
        {
            get; set;
        }

        /// <summary>
        /// Indicates the icon that should be displayed in the system tray for this
        /// application.
        /// </summary>
        public Icon TrayIcon
        {
            get; set;
        }

        /// <summary>
        /// Returns the default tray icon used by this application when it is run for
        /// the first time.
        /// </summary>
        public Icon DefaultTrayIcon
        {
            [DebuggerStepThrough]
            get { return _defaultTrayIcon; }
        }

        /// <summary>
        /// Event which occurs when the configuration is saved.
        /// </summary>
        public event EventHandler Saved
        {
            add { _savedEvent += value; }
            remove { _savedEvent -= value; }
        }

        /// <summary>
        /// Causes the <see cref="Saved"/> event to be raised.
        /// </summary>
        private void RaiseSavedEvent()
        {
            EventHandler eventHandler = _savedEvent;

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Saves the application configuration to the configuration file.
        /// </summary>
        public void Save()
        {
            if (Directory.Exists(ConfigurationFileDirectory) == false)
            {
                Directory.CreateDirectory(ConfigurationFileDirectory);
            }

            using (Stream configurationFileStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(configurationFileStream))
                {
                    writer.WriteStartElement("Configuration");
                    writer.WriteElementString("Version", "1.0");
                    writer.WriteElementString("HotkeyKey", ((int)Hotkey.Key).ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("HotkeyAlt", Hotkey.Alt.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("HotkeyShift", Hotkey.Shift.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("HotkeyControl", Hotkey.Control.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("HotkeyWindows", Hotkey.Windows.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("HotkeyEnabled", HotkeyEnabled.ToString(CultureInfo.InvariantCulture));
                    writer.WriteEndElement();
                }

                byte[] xmlFileBytes = new byte[configurationFileStream.Length];

                configurationFileStream.Position = 0;
                configurationFileStream.Read(xmlFileBytes, 0, xmlFileBytes.Length);

                File.WriteAllBytes(ConfigurationFilePath, xmlFileBytes);
            }

            if (TrayIcon != null)
            {
                if (TrayIcon == _defaultTrayIcon)
                {
                    //If we're using the default tray icon, make sure that there's not
                    //and icon saved into the configuration directory.  That way, if the
                    //icon is updated in future versions, the user will see the new icon.
                    File.Delete(TrayIconFilePath);
                }
                else
                {
                    using (FileStream iconFile = new FileStream(TrayIconFilePath, FileMode.Create))
                    {
                        TrayIcon.Save(iconFile);
                    }
                }
            }

            RaiseSavedEvent();
        }

        /// <summary>
        /// Loads the default settings for the application into the configuration service.
        /// </summary>
        private void LoadDefaultSettings()
        {
            TrayIcon = _defaultTrayIcon;

            Hotkey = new Hotkey
            {
                Alt = false,
                Control = false,
                Shift = false,
                Key = Keys.W,
                Windows = true
            };

            HotkeyEnabled = true;
        }

        /// <summary>
        /// Loads the application configuration from the current user's 
        /// application data directory, if there is any configuration
        /// stored there for this application.
        /// </summary>
        public void Load()
        {
            LoadConfiguredTrayIcon();
            LoadConfiguredSettings();
        }

        /// <summary>
        /// Loads the configured tray icon for the user.
        /// </summary>
        private void LoadConfiguredTrayIcon()
        {
            if (File.Exists(TrayIconFilePath))
            {
                TrayIcon = new Icon(TrayIconFilePath);
            }
        }

        /// <summary>
        /// Loads the configured application settings for the user.
        /// </summary>
        private void LoadConfiguredSettings()
        {
            if (File.Exists(ConfigurationFilePath) == false)
            {
                return;
            }

            Hotkey hotkey = new Hotkey();
            Boolean hotkeyEnabled;

            try
            {
                XPathDocument xmlDocument = new XPathDocument(ConfigurationFilePath);
                XPathNavigator xPathNavigator = xmlDocument.CreateNavigator();

                hotkey.Key = (Keys)(xPathNavigator.SelectSingleNode("/Configuration/HotkeyKey").ValueAsInt);
                hotkey.Alt = Boolean.Parse(xPathNavigator.SelectSingleNode("/Configuration/HotkeyAlt").Value);
                hotkey.Shift = Boolean.Parse(xPathNavigator.SelectSingleNode("/Configuration/HotkeyShift").Value);
                hotkey.Control = Boolean.Parse(xPathNavigator.SelectSingleNode("/Configuration/HotkeyControl").Value);
                hotkey.Windows = Boolean.Parse(xPathNavigator.SelectSingleNode("/Configuration/HotkeyWindows").Value);
                hotkeyEnabled = Boolean.Parse(xPathNavigator.SelectSingleNode("/Configuration/HotkeyEnabled").Value);
            }
            catch (Exception exception)
            {
                Trace.WriteLine("Error loading configuration from file " + ConfigurationFilePath);
                Trace.WriteLine(exception);
                return;
            }

            Hotkey = hotkey;
            HotkeyEnabled = hotkeyEnabled;
        }

        /// <summary>
        /// Gets the directory that the configuration file should be stored in for the
        /// current user.
        /// </summary>
        private static String ConfigurationFileDirectory
        {
            [DebuggerStepThrough]
            get
            {
                String localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                return Path.Combine(localApplicationDataPath, Resources.ConfigurationSubdirectory);
            }
        }

        /// <summary>
        /// Gets the full path and file name of the configuration file that stores the
        /// application's settings for the current user.
        /// </summary>
        /// <remarks>
        /// This file may not exist if the user has not made any changes to their
        /// configuration.
        /// </remarks>
        private static String ConfigurationFilePath
        {
            [DebuggerStepThrough]
            get
            {
                return Path.Combine(ConfigurationFileDirectory, Resources.ConfigurationFileName);
            }
        }

        /// <summary>
        /// Gets the full path and file name to the tray icon that is configured
        /// for the current user.
        /// </summary>
        /// <remarks>
        /// This file may not exist if the user has not made any changes to their
        /// configuration.
        /// </remarks>
        private static String TrayIconFilePath
        {
            [DebuggerStepThrough]
            get
            {
                return Path.Combine(ConfigurationFileDirectory, "TrayIcon.ico");
            }
        }

        /// <summary>
        /// Dictionary of valid keys that can be selected as a hotkey, and the display
        /// test that is shown to the user for the key.
        /// </summary>
        public static readonly Dictionary<Keys, String> ValidHotkeys =
            new Dictionary<Keys, String>
                {
                    {Keys.A, "A"},
                    {Keys.B, "B"},
                    {Keys.C, "C"},
                    {Keys.D, "D"},
                    {Keys.E, "E"},
                    {Keys.F, "F"},
                    {Keys.G, "G"},
                    {Keys.H, "H"},
                    {Keys.I, "I"},
                    {Keys.J, "J"},
                    {Keys.K, "K"},
                    {Keys.L, "L"},
                    {Keys.M, "M"},
                    {Keys.N, "N"},
                    {Keys.O, "O"},
                    {Keys.P, "P"},
                    {Keys.Q, "Q"},
                    {Keys.R, "R"},
                    {Keys.S, "S"},
                    {Keys.T, "T"},
                    {Keys.U, "U"},
                    {Keys.V, "V"},
                    {Keys.W, "W"},
                    {Keys.X, "X"},
                    {Keys.Y, "Y"},
                    {Keys.Z, "Z"},
                    {Keys.F1, "F1"},
                    {Keys.F2, "F2"},
                    {Keys.F3, "F3"},
                    {Keys.F4, "F4"},
                    {Keys.F5, "F5"},
                    {Keys.F6, "F6"},
                    {Keys.F7, "F7"},
                    {Keys.F8, "F8"},
                    {Keys.F9, "F9"},
                    {Keys.F10, "F10"},
                    {Keys.F11, "F11"},
                    {Keys.F12, "F12"},
                    {Keys.Oemtilde, "~"},
                    {Keys.Tab, "Tab"},
                    {Keys.OemMinus, "-"},
                    {Keys.Oemplus, "="},
                };
    }
}

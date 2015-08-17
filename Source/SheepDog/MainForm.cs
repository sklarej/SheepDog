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
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows.Forms;
using SheepDog.Properties;
using SheepDog.WindowsApi;

namespace SheepDog
{
    /// <summary>
    /// Main form for the application.  This form is never shown to the user.
    /// It's purpose is to 1) host the NotifyIcon that appears in the system
    /// tray, 2) detect when the registered hotkey is pressed and reposition
    /// windows when it is, and 3) react to the user choosing menu items in
    /// the NotifyIcon's context menu.
    /// </summary>
    public partial class MainForm : Form
    {
        public IServiceProvider _serviceProvider;
        private OptionsForm _optionsForm;
        private AboutForm _aboutForm;

        /// <summary>
        /// Creates a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Visible = false;
            Icon = Resources.DefaultTrayIcon;
        }

        /// <summary>5
        /// Creates a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm(IServiceProvider serviceProvider) : this()
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Initializes the appearance of the components on the form and prepares it
        /// for running.
        /// </summary>
        public void Initialize()
        {
            Text = Resources.ApplicationTitle;

            HotkeyService.HotkeyChanged += HotkeyChangedEventHandler;
            UpdateUiWithHotkey(HotkeyService.Hotkey);
            notifyIcon.Visible = true;
        }

        /// <summary>
        /// Overridden logic which allows the form to detect when the registered
        /// hotkey has been pressed.
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == User32.WM_HOTKEY)
            {
                RepositionWindows();
            }
        }

        /// <summary>
        /// Handles when the hotkey has been changed for the application, and updates the UI to
        /// reflect the new hotkey.
        /// </summary>
        private void HotkeyChangedEventHandler(object sender, HotkeyChangedEventArgs e)
        {
            UpdateUiWithHotkey(e.Hotkey);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateUiWithHotkey(Hotkey hotkey)
        {
            String hotkeyText = String.Empty;
            
            if (hotkey != null)
            {
                hotkeyText = hotkey.ToString();
            }

            //Update the shortcut shown in the system tray context menu
            repositionWindowsMenuItem.ShortcutKeyDisplayString = hotkeyText;

            //Update the tooltip for the system tray icon
            String trayTooltip = Resources.ApplicationTitle + " - " + Resources.ApplicationSubTitle;

            if (String.IsNullOrEmpty(hotkeyText) == false)
            {
                trayTooltip += "\n\nHoykey: " + hotkeyText;
            }

            notifyIcon.Text = trayTooltip;

            //Update the icon shown in the system tray
            if (ConfigurationService.TrayIcon != null)
            {
                notifyIcon.Icon = ConfigurationService.TrayIcon;
            }
        }

        /// <summary>
        /// Handles the Click event for the Close button.  This closes the form when
        /// the menu option is clicked.
        /// </summary>
        private void CloseMenuItemClickEventHandler(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///  Handles the Click event for the Reposition Windows button.
        /// </summary>
        private void RepositionWindowsMenuItemClickEventHandler(object sender, EventArgs e)
        {
            RepositionWindows();
        }

        /// <summary>
        ///  Handles the Click event for the Reposition Windows button.
        /// </summary>
        private void OptionsMenuItemClickEventHandler(object sender, EventArgs e)
        {
            ShowOptionsForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AboutMenuItemClickEventHandler(object sender, EventArgs e)
        {
            ShowAboutForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void TrayIconDoubleClickEventHandler(object sender, EventArgs e)
        {
            RepositionWindows();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RepositionWindows()
        {
            int windowCount = OffScreenWindowRepositioner.RepositionWindows();

            notifyIcon.BalloonTipTitle = "SheepDog";

            if (windowCount == 0)
            {
                notifyIcon.BalloonTipText = "No offscreen windows detected";
            }
            else if (windowCount == 1)
            {
                notifyIcon.BalloonTipText = windowCount + " window repositioned";
            }
            else
            {
                notifyIcon.BalloonTipText = windowCount + " windows repositioned";
            }

            notifyIcon.ShowBalloonTip(2000);
        }

        /// <summary>
        /// Opens the Options form.
        /// </summary>
        private void ShowOptionsForm()
        {
            if (_optionsForm == null)
            {
                using (_optionsForm = new OptionsForm(_serviceProvider))
                {
                    _optionsForm.ShowDialog(this);
                }

                _optionsForm = null;
            }
            else
            {
                //The options form is already visible, so just bring it to the front
                _optionsForm.BringToFront();
            }
        }

        /// <summary>
        /// Opens the About form.
        /// </summary>
        private void ShowAboutForm()
        {
            if (_aboutForm == null)
            {
                using (_aboutForm = new AboutForm(_serviceProvider))
                {
                    _aboutForm.ShowDialog(this);
                }

                _aboutForm = null;
            }
            else
            {
                //The about form is already visible, so just bring it to the front
                _aboutForm.BringToFront();
            }
        }

        /// <summary>
        /// Override which ensures that the main form is never visible to the user.
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            Visible = false;
        }

        #region Services

        /// <summary>
        /// Gets the <see cref="ConfigurationService"/> service.
        /// </summary>
        public ConfigurationService ConfigurationService
        {
            [DebuggerStepThrough]
            get { return _serviceProvider.GetService(typeof(ConfigurationService)) as ConfigurationService; }
        }

        /// <summary>
        /// Gets the <see cref="GlobalHotKeyService"/> service.
        /// </summary>
        public GlobalHotkeyService HotkeyService
        {
            [DebuggerStepThrough]
            get { return _serviceProvider.GetService(typeof (GlobalHotkeyService)) as GlobalHotkeyService; }
        }

        #endregion

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

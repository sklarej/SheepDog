#region MIT License
/*
MIT License
Copyright (c) 2009-2018 Joshua Sklare
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
using SheepDog.Properties;
using SheepDog.WindowsApi;

namespace SheepDog
{
    /// <summary>
    /// Controls registration and un-registration of the global hotkey for
    /// this application. 
    /// </summary>
    public class GlobalHotkeyService : BaseControl, IDisposable
    {
        private const String HotkeyAtomSuffix = "HotkeyAtom";

        private Int16 _hotkeyAtom;
        private IntPtr _mainFormHwnd;
        private Hotkey _hotkey;
        private EventHandler<HotkeyChangedEventArgs> _hotkeyChangedEvent;

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalHotkeyService"/> class.
        /// </summary>
        public GlobalHotkeyService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// Finalizes the instance of the <see cref="GlobalHotkeyService"/> class.
        /// </summary>
        ~GlobalHotkeyService()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes the hotkey service and loads the current hotkey from the 
        /// configuration service.
        /// </summary>
        public void Initialize()
        {
            _mainFormHwnd = MainForm.Handle;
            ConfigurationService.Saved += ConfigurationSavedEventHandler;
            LoadHotkeyFromConfiguration();
        }

        /// <summary>
        /// Loads the current hotkey from the configuration service.
        /// </summary>
        private void LoadHotkeyFromConfiguration()
        {
            if (ConfigurationService.HotkeyEnabled)
            {
                Hotkey = ConfigurationService.Hotkey;
            }
            else
            {
                Hotkey = null;
            }
        }

        /// <summary>
        /// Handles the Saved event for the configuration service.  This ensures that the
        /// current registered hotkey reflects the one that is set by the user.
        /// </summary>
        private void ConfigurationSavedEventHandler(object sender, EventArgs e)
        {
            LoadHotkeyFromConfiguration();
        }

        /// <summary>
        /// Specifies the global hotkey which activates this application.
        /// </summary>
        public Hotkey Hotkey
        {
            get
            {
                return _hotkey;
            }
            set
            {
                UnregisterHotkey();
                _hotkey = value;
                RegisterHotkey();

                RaiseHotkeyChangedEvent(Hotkey);
            }
        }

        /// <summary>
        /// Occurs when the registered global hotkey is changed.
        /// </summary>
        public event EventHandler<HotkeyChangedEventArgs> HotkeyChanged
        {
            add { _hotkeyChangedEvent += value; }
            remove { _hotkeyChangedEvent -= value; }
        }

        /// <summary>
        /// Causes the <see cref="HotkeyChanged"/> event to be raised.
        /// </summary>
        /// <param name="hotkey"></param>
        private void RaiseHotkeyChangedEvent(Hotkey hotkey)
        {
            _hotkeyChangedEvent?.Invoke(this, new HotkeyChangedEventArgs(hotkey));
        }

        /// <summary>
        /// Registers the currently set hotkey as a global hotkey.
        /// </summary>
        private void RegisterHotkey()
        {
            if (Hotkey == null)
            {
                return;
            }

            _hotkeyAtom = Kernel32.GlobalAddAtom(Resources.ApplicationTitle + HotkeyAtomSuffix);

            bool isRegistered = GlobalHotKeyInterop.RegisterGlobalHotkey(_hotkeyAtom, _mainFormHwnd, Hotkey);

            if (isRegistered == false)
            {
                UnregisterHotkey();
            }
        }

        /// <summary>
        /// Unregisterers the last global hotkey registered by this controller.
        /// </summary>
        private void UnregisterHotkey()
        {
            if (_hotkey != null)
            {
                GlobalHotKeyInterop.UnregisterGlobalHotkey(_hotkeyAtom, _mainFormHwnd);
                _hotkey = null;
            }

            if (_hotkeyAtom != 0)
            {
                Kernel32.GlobalDeleteAtom(_hotkeyAtom);
                _hotkeyAtom = 0;
            }
        }

        /// <summary>
        /// Cleans up the service by unregistering the hotkey.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up the service by unregistering the hotkey.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether managed resources should be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Dispose managed resource here.
            }

            UnregisterHotkey();
        }

        #region Services

        /// <summary>
        /// Gets the <see cref="ConfigurationService"/> service.
        /// </summary>
        public ConfigurationService ConfigurationService
        {
            [DebuggerStepThrough]
            get { return GetService(typeof(ConfigurationService)) as ConfigurationService; }
        }

        /// <summary>
        /// Gets the <see cref="MainForm"/> service.
        /// </summary>
        public MainForm MainForm
        {
            [DebuggerStepThrough]
            get { return GetService(typeof(MainForm)) as MainForm; }
        }

        #endregion
    }
}

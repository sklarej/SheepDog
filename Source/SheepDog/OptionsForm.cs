#region MIT License
/*
MIT License
Copyright (c) 2008 Josh Sklare
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
using System.Drawing;
using System.Windows.Forms;
using SheepDog.Properties;
using SheepDog.WindowsApi;

namespace SheepDog
{
	/// <summary>
	/// Form which allows the user to set the configurable options for this
	/// application.
	/// </summary>
	public partial class OptionsForm : Form
	{
		private readonly IServiceProvider _serviceProvider;
		private Icon _currentIcon;

		/// <summary>
		/// Creates a new instance of the <see cref="OptionsForm"/> class.
		/// </summary>
		public OptionsForm()
		{
			InitializeComponent();

			foreach (var availableKey in ConfigurationService.ValidHotkeys)
			{
				comboBoxKey.Items.Add(availableKey.Value);
			}
		}

		/// <summary>
		/// Creates a new instance of the <see cref="OptionsForm"/> class.
		/// </summary>
		public OptionsForm(IServiceProvider serviceProvider) : this()
		{
			_serviceProvider = serviceProvider;
		}

		/// <summary>
		/// Initializes the form's title and icon as it is loading.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Text = MainForm.Text + " - " + Text;
			Icon = MainForm.Icon;

			checkBoxHotkeyEnabled.Checked = ConfigurationService.HotkeyEnabled;
			Hotkey = ConfigurationService.Hotkey;

			_currentIcon = ConfigurationService.TrayIcon;
			pictureBoxIcon.Image = _currentIcon.ToBitmap();
		}

		/// <summary>
		/// Saves off the current options set by the user to the configuration
		/// service when the form closes.
		/// </summary>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			//Prevent saving changes in situations where the application is closing down.
			if ((DialogResult == DialogResult.OK) || (e.CloseReason == CloseReason.UserClosing))
			{
				if (buttonClose.Enabled == false)
				{
					e.Cancel = true;
					return;
				}

				ConfigurationService.TrayIcon = _currentIcon;
				ConfigurationService.Hotkey = Hotkey;
				ConfigurationService.HotkeyEnabled = checkBoxHotkeyEnabled.Checked;
				ConfigurationService.Save();
			}
		}

		/// <summary>
		/// Gets or sets the hotkey that is displayed on this configuration form.
		/// </summary>
		protected Hotkey Hotkey
		{
			get
			{
				Keys selectedKey = Keys.None;

				foreach (var availableKey in ConfigurationService.ValidHotkeys)
				{
					if (String.Equals(availableKey.Value, comboBoxKey.Text))
					{
						selectedKey = availableKey.Key;
						break;
					}
				}

				Hotkey hotkey = new Hotkey
				{
					Shift = checkBoxShift.Checked,
					Alt = checkBoxAlt.Checked,
					Control = checkBoxControl.Checked,
					Windows = checkBoxWindows.Checked,
					Key = selectedKey
				};

				return hotkey;
			}
			set
			{
				checkBoxShift.Checked = value.Shift;
				checkBoxAlt.Checked = value.Alt;
				checkBoxControl.Checked = value.Control;
				checkBoxWindows.Checked = value.Windows;
				comboBoxKey.SelectedItem = ConfigurationService.ValidHotkeys[value.Key];

				UpdateHotkeyUi();
			}
		}

		/// <summary>
		/// Updates the hotkey related UI components by enabling or disabling them,
		/// to reflect the current setting of the <see cref="checkBoxHotkeyEnabled"/>
		/// checkbox.
		/// </summary>
		private void UpdateHotkeyUi()
		{
			Boolean enabled = checkBoxHotkeyEnabled.Checked;

			checkBoxShift.Enabled = enabled;
			checkBoxAlt.Enabled = enabled;
			checkBoxControl.Enabled = enabled;
			checkBoxWindows.Enabled = enabled;
			comboBoxKey.Enabled = enabled;
			labelPlus.Enabled = enabled;

			bool validHotkey = false;

			if (checkBoxHotkeyEnabled.Checked)
			{
				validHotkey = IsHotkeyAvailable();
			}

			panelInvalidHotkey.Visible = !validHotkey;
			buttonClose.Enabled = validHotkey;
		}

		/// <summary>
		/// Returns whether or not the hotkey currently set on this form is valid
		/// to be registered as a global hotkey.
		/// </summary>
		private Boolean IsHotkeyAvailable()
		{
			if (Hotkey.Equals(GlobalHotkeyService.Hotkey))
			{
				return true;
			}

			return GlobalHotKeyInterop.IsHotkeyAvailable(Hotkey);
		}

		/// <summary>
		/// Event handler that occurs whenever the user changes any of the hotkey
		/// related settings on the options form.  This ensures that the 
		/// </summary>
		private void HotkeySettingChangedEventHandler(object sender, EventArgs e)
		{
			UpdateHotkeyUi();
		}

		/// <summary>
		/// Event handler for the Click event of the "Default Icon" button.  This loads
		/// the default Icon resource.
		/// </summary>
		private void DefaultIconButtonClickEventHandler(object sender, EventArgs e)
		{
			SetIcon(ConfigurationService.DefaultTrayIcon);
		}

		/// <summary>
		/// Event handler for the Click event of the "Load Icon" button.  This shows
		/// a file dialog that allows the user to choose an icon file, and then loads
		/// the icon from the selected file.
		/// </summary>
		private void LoadIconClickEventHandler(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Title = Resources.ApplicationTitle + " - Select an icon file...";
				dialog.Multiselect = false;
				dialog.DefaultExt = "*.ico";
				dialog.Filter = "Icon Files(*.ico)|*.ico";
				dialog.CheckFileExists = true;

				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					LoadIconFromFile(dialog.FileName);
				}
			}
		}

		/// <summary>
		/// Loads the icon from the specified file.
		/// </summary>
		/// <remarks>
		/// A 16x16 icon is preferably loaded, because that is the size that
		/// a tray icon is.  However, if there is not an icon of that size in the icon file, then an icon will still
		/// be loaded from it.
		/// </remarks>
		private void LoadIconFromFile(string fileName)
		{
			Icon icon = new Icon(fileName, 16, 16);
			SetIcon(icon);
		}

		/// <summary>
		/// Sets the icon loaded on this form to the specified icon.
		/// </summary>
		private void SetIcon(Icon icon)
		{
			_currentIcon = icon;
			pictureBoxIcon.Image = _currentIcon.ToBitmap();
		}

		#region Services

		/// <summary>
		/// Gets the <see cref="ConfigurationService"/> service.
		/// </summary>
		public ConfigurationService ConfigurationService
		{
			[DebuggerStepThrough]
			get { return _serviceProvider.GetService(typeof (ConfigurationService)) as ConfigurationService; }
		}

		/// <summary>
		/// Gets the <see cref="GlobalHotkeyService"/> service.
		/// </summary>
		public GlobalHotkeyService GlobalHotkeyService
		{
			[DebuggerStepThrough]
			get { return _serviceProvider.GetService(typeof(GlobalHotkeyService)) as GlobalHotkeyService; }
		}

		/// <summary>
		/// Gets the <see cref="MainForm"/> service.
		/// </summary>
		public MainForm MainForm
		{
			[DebuggerStepThrough]
			get { return _serviceProvider.GetService(typeof (MainForm)) as MainForm; }
		}

		#endregion
	}
}

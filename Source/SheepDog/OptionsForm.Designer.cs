#region MIT License
/*
MIT License
Copyright (c) 2009 Josh Sklare
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

namespace SheepDog
{
	partial class OptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBoxHotkey = new System.Windows.Forms.GroupBox();
			this.comboBoxKey = new System.Windows.Forms.ComboBox();
			this.groupBoxHotkeyModifiers = new System.Windows.Forms.GroupBox();
			this.checkBoxAlt = new System.Windows.Forms.CheckBox();
			this.checkBoxControl = new System.Windows.Forms.CheckBox();
			this.checkBoxShift = new System.Windows.Forms.CheckBox();
			this.checkBoxWindows = new System.Windows.Forms.CheckBox();
			this.checkBoxHotkeyEnabled = new System.Windows.Forms.CheckBox();
			this.labelPlus = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panelInvalidHotkey = new System.Windows.Forms.Panel();
			this.labelInvalidHotkey = new System.Windows.Forms.Label();
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.groupBoxIcon = new System.Windows.Forms.GroupBox();
			this.buttonUseDefaultIcon = new System.Windows.Forms.Button();
			this.buttonLoadIcon = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBoxHotkey.SuspendLayout();
			this.groupBoxHotkeyModifiers.SuspendLayout();
			this.panelInvalidHotkey.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.groupBoxIcon.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxHotkey
			// 
			this.groupBoxHotkey.BackColor = System.Drawing.SystemColors.Control;
			this.groupBoxHotkey.Controls.Add(this.comboBoxKey);
			this.groupBoxHotkey.Controls.Add(this.groupBoxHotkeyModifiers);
			this.groupBoxHotkey.Controls.Add(this.checkBoxHotkeyEnabled);
			this.groupBoxHotkey.Controls.Add(this.labelPlus);
			this.groupBoxHotkey.Location = new System.Drawing.Point(12, 12);
			this.groupBoxHotkey.Name = "groupBoxHotkey";
			this.groupBoxHotkey.Size = new System.Drawing.Size(199, 159);
			this.groupBoxHotkey.TabIndex = 0;
			this.groupBoxHotkey.TabStop = false;
			this.groupBoxHotkey.Text = "Global Hotkey";
			// 
			// comboBoxKey
			// 
			this.comboBoxKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxKey.FormattingEnabled = true;
			this.comboBoxKey.Location = new System.Drawing.Point(122, 91);
			this.comboBoxKey.Name = "comboBoxKey";
			this.comboBoxKey.Size = new System.Drawing.Size(68, 21);
			this.comboBoxKey.TabIndex = 3;
			this.comboBoxKey.SelectedValueChanged += new System.EventHandler(this.HotkeySettingChangedEventHandler);
			// 
			// groupBoxHotkeyModifiers
			// 
			this.groupBoxHotkeyModifiers.Controls.Add(this.checkBoxAlt);
			this.groupBoxHotkeyModifiers.Controls.Add(this.checkBoxControl);
			this.groupBoxHotkeyModifiers.Controls.Add(this.checkBoxShift);
			this.groupBoxHotkeyModifiers.Controls.Add(this.checkBoxWindows);
			this.groupBoxHotkeyModifiers.Location = new System.Drawing.Point(9, 42);
			this.groupBoxHotkeyModifiers.Name = "groupBoxHotkeyModifiers";
			this.groupBoxHotkeyModifiers.Size = new System.Drawing.Size(88, 109);
			this.groupBoxHotkeyModifiers.TabIndex = 6;
			this.groupBoxHotkeyModifiers.TabStop = false;
			// 
			// checkBoxAlt
			// 
			this.checkBoxAlt.AutoSize = true;
			this.checkBoxAlt.Location = new System.Drawing.Point(6, 41);
			this.checkBoxAlt.Name = "checkBoxAlt";
			this.checkBoxAlt.Size = new System.Drawing.Size(38, 17);
			this.checkBoxAlt.TabIndex = 1;
			this.checkBoxAlt.Text = "Alt";
			this.checkBoxAlt.UseVisualStyleBackColor = true;
			this.checkBoxAlt.CheckedChanged += new System.EventHandler(this.HotkeySettingChangedEventHandler);
			// 
			// checkBoxControl
			// 
			this.checkBoxControl.AutoSize = true;
			this.checkBoxControl.Location = new System.Drawing.Point(6, 18);
			this.checkBoxControl.Name = "checkBoxControl";
			this.checkBoxControl.Size = new System.Drawing.Size(41, 17);
			this.checkBoxControl.TabIndex = 0;
			this.checkBoxControl.Text = "Ctrl";
			this.checkBoxControl.UseVisualStyleBackColor = true;
			this.checkBoxControl.CheckedChanged += new System.EventHandler(this.HotkeySettingChangedEventHandler);
			// 
			// checkBoxShift
			// 
			this.checkBoxShift.AutoSize = true;
			this.checkBoxShift.Location = new System.Drawing.Point(6, 64);
			this.checkBoxShift.Name = "checkBoxShift";
			this.checkBoxShift.Size = new System.Drawing.Size(47, 17);
			this.checkBoxShift.TabIndex = 2;
			this.checkBoxShift.Text = "Shift";
			this.checkBoxShift.UseVisualStyleBackColor = true;
			this.checkBoxShift.CheckedChanged += new System.EventHandler(this.HotkeySettingChangedEventHandler);
			// 
			// checkBoxWindows
			// 
			this.checkBoxWindows.AutoSize = true;
			this.checkBoxWindows.Location = new System.Drawing.Point(6, 87);
			this.checkBoxWindows.Name = "checkBoxWindows";
			this.checkBoxWindows.Size = new System.Drawing.Size(70, 17);
			this.checkBoxWindows.TabIndex = 3;
			this.checkBoxWindows.Text = "Windows";
			this.checkBoxWindows.UseVisualStyleBackColor = true;
			this.checkBoxWindows.CheckedChanged += new System.EventHandler(this.HotkeySettingChangedEventHandler);
			// 
			// checkBoxHotkeyEnabled
			// 
			this.checkBoxHotkeyEnabled.AutoSize = true;
			this.checkBoxHotkeyEnabled.Location = new System.Drawing.Point(14, 19);
			this.checkBoxHotkeyEnabled.Name = "checkBoxHotkeyEnabled";
			this.checkBoxHotkeyEnabled.Size = new System.Drawing.Size(65, 17);
			this.checkBoxHotkeyEnabled.TabIndex = 1;
			this.checkBoxHotkeyEnabled.Text = "Enabled";
			this.checkBoxHotkeyEnabled.UseVisualStyleBackColor = true;
			this.checkBoxHotkeyEnabled.CheckedChanged += new System.EventHandler(this.HotkeySettingChangedEventHandler);
			// 
			// labelPlus
			// 
			this.labelPlus.AutoSize = true;
			this.labelPlus.Location = new System.Drawing.Point(103, 94);
			this.labelPlus.Name = "labelPlus";
			this.labelPlus.Size = new System.Drawing.Size(13, 13);
			this.labelPlus.TabIndex = 5;
			this.labelPlus.Text = "+";
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonClose.Location = new System.Drawing.Point(270, 177);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// panelInvalidHotkey
			// 
			this.panelInvalidHotkey.BackColor = System.Drawing.Color.Coral;
			this.panelInvalidHotkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelInvalidHotkey.Controls.Add(this.labelInvalidHotkey);
			this.panelInvalidHotkey.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelInvalidHotkey.Location = new System.Drawing.Point(0, 0);
			this.panelInvalidHotkey.Name = "panelInvalidHotkey";
			this.panelInvalidHotkey.Size = new System.Drawing.Size(353, 25);
			this.panelInvalidHotkey.TabIndex = 3;
			this.panelInvalidHotkey.Visible = false;
			// 
			// labelInvalidHotkey
			// 
			this.labelInvalidHotkey.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelInvalidHotkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInvalidHotkey.ForeColor = System.Drawing.Color.Maroon;
			this.labelInvalidHotkey.Location = new System.Drawing.Point(0, 0);
			this.labelInvalidHotkey.Name = "labelInvalidHotkey";
			this.labelInvalidHotkey.Size = new System.Drawing.Size(351, 23);
			this.labelInvalidHotkey.TabIndex = 0;
			this.labelInvalidHotkey.Text = "Hotkey Is Unavailable";
			this.labelInvalidHotkey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pictureBoxIcon.Location = new System.Drawing.Point(48, 31);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxIcon.TabIndex = 4;
			this.pictureBoxIcon.TabStop = false;
			// 
			// groupBoxIcon
			// 
			this.groupBoxIcon.BackColor = System.Drawing.SystemColors.Control;
			this.groupBoxIcon.Controls.Add(this.panel1);
			this.groupBoxIcon.Controls.Add(this.buttonUseDefaultIcon);
			this.groupBoxIcon.Controls.Add(this.buttonLoadIcon);
			this.groupBoxIcon.Location = new System.Drawing.Point(217, 12);
			this.groupBoxIcon.Name = "groupBoxIcon";
			this.groupBoxIcon.Size = new System.Drawing.Size(128, 159);
			this.groupBoxIcon.TabIndex = 7;
			this.groupBoxIcon.TabStop = false;
			this.groupBoxIcon.Text = "Icon";
			// 
			// buttonUseDefaultIcon
			// 
			this.buttonUseDefaultIcon.Location = new System.Drawing.Point(6, 130);
			this.buttonUseDefaultIcon.Name = "buttonUseDefaultIcon";
			this.buttonUseDefaultIcon.Size = new System.Drawing.Size(114, 23);
			this.buttonUseDefaultIcon.TabIndex = 6;
			this.buttonUseDefaultIcon.Text = "Use Default Icon";
			this.buttonUseDefaultIcon.UseVisualStyleBackColor = true;
			this.buttonUseDefaultIcon.Click += new System.EventHandler(this.DefaultIconButtonClickEventHandler);
			// 
			// buttonLoadIcon
			// 
			this.buttonLoadIcon.Location = new System.Drawing.Point(6, 102);
			this.buttonLoadIcon.Name = "buttonLoadIcon";
			this.buttonLoadIcon.Size = new System.Drawing.Size(114, 23);
			this.buttonLoadIcon.TabIndex = 5;
			this.buttonLoadIcon.Text = "Load Icon...";
			this.buttonLoadIcon.UseVisualStyleBackColor = true;
			this.buttonLoadIcon.Click += new System.EventHandler(this.LoadIconClickEventHandler);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.pictureBoxIcon);
			this.panel1.Location = new System.Drawing.Point(6, 15);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(114, 81);
			this.panel1.TabIndex = 7;
			// 
			// OptionsForm
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(353, 203);
			this.Controls.Add(this.panelInvalidHotkey);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.groupBoxHotkey);
			this.Controls.Add(this.groupBoxIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "OptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Options";
			this.groupBoxHotkey.ResumeLayout(false);
			this.groupBoxHotkey.PerformLayout();
			this.groupBoxHotkeyModifiers.ResumeLayout(false);
			this.groupBoxHotkeyModifiers.PerformLayout();
			this.panelInvalidHotkey.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.groupBoxIcon.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxHotkey;
		private System.Windows.Forms.CheckBox checkBoxWindows;
		private System.Windows.Forms.CheckBox checkBoxShift;
		private System.Windows.Forms.CheckBox checkBoxAlt;
		private System.Windows.Forms.CheckBox checkBoxControl;
		private System.Windows.Forms.Label labelPlus;
		private System.Windows.Forms.GroupBox groupBoxHotkeyModifiers;
		private System.Windows.Forms.CheckBox checkBoxHotkeyEnabled;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ComboBox comboBoxKey;
		private System.Windows.Forms.Panel panelInvalidHotkey;
		private System.Windows.Forms.Label labelInvalidHotkey;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.GroupBox groupBoxIcon;
		private System.Windows.Forms.Button buttonLoadIcon;
		private System.Windows.Forms.Button buttonUseDefaultIcon;
		private System.Windows.Forms.Panel panel1;
	}
}
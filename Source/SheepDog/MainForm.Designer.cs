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
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.repositionWindowsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeperator = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenu;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.DoubleClick += new System.EventHandler(this.TrayIconDoubleClickEventHandler);
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repositionWindowsMenuItem,
            this.menuItemSeperator,
            this.menuItemOptions,
            this.menuItemAbout,
            this.menuItemClose});
			this.contextMenu.Name = "menu";
			this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenu.ShowImageMargin = false;
			this.contextMenu.Size = new System.Drawing.Size(174, 120);
			// 
			// repositionWindowsMenuItem
			// 
			this.repositionWindowsMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.repositionWindowsMenuItem.Name = "repositionWindowsMenuItem";
			this.repositionWindowsMenuItem.ShortcutKeyDisplayString = "";
			this.repositionWindowsMenuItem.Size = new System.Drawing.Size(173, 22);
			this.repositionWindowsMenuItem.Text = "&Reposition Windows";
			this.repositionWindowsMenuItem.Click += new System.EventHandler(this.RepositionWindowsMenuItemClickEventHandler);
			// 
			// menuItemSeperator
			// 
			this.menuItemSeperator.Name = "menuItemSeperator";
			this.menuItemSeperator.Size = new System.Drawing.Size(170, 6);
			// 
			// menuItemOptions
			// 
			this.menuItemOptions.Name = "menuItemOptions";
			this.menuItemOptions.Size = new System.Drawing.Size(173, 22);
			this.menuItemOptions.Text = "&Options...";
			this.menuItemOptions.Click += new System.EventHandler(this.OptionsMenuItemClickEventHandler);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.Size = new System.Drawing.Size(173, 22);
			this.menuItemAbout.Text = "&About...";
			this.menuItemAbout.Click += new System.EventHandler(this.AboutMenuItemClickEventHandler);
			// 
			// menuItemClose
			// 
			this.menuItemClose.Name = "menuItemClose";
			this.menuItemClose.Size = new System.Drawing.Size(173, 22);
			this.menuItemClose.Text = "&Close";
			this.menuItemClose.Click += new System.EventHandler(this.CloseMenuItemClickEventHandler);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(151, 30);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem repositionWindowsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuItemClose;
		private System.Windows.Forms.ToolStripSeparator menuItemSeperator;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private System.Windows.Forms.ToolStripMenuItem menuItemOptions;
	}
}


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
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using SheepDog.Properties;

namespace SheepDog
{
	/// <summary>
	/// Form which displays information about this application.
	/// </summary>
	partial class AboutForm : Form
	{
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Creates a new instance of the <see cref="AboutForm"/> class.
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();
			Text = String.Format(CultureInfo.InvariantCulture, "About {0}", Resources.ApplicationTitle);
			labelProductName.Text = Resources.ApplicationTitle + " - " + Resources.ApplicationSubTitle;
			labelVersion.Text = String.Format(CultureInfo.InvariantCulture, "Version {0}", AssemblyVersion);
			labelCopyright.Text = Resources.ApplicationCopyright;
			labelWebSite.Text = Resources.ApplicationWebSite;
			textBoxDescription.Text = Resources.ApplicationDescription + "\r\n\r\n" + Resources.License;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="AboutForm"/> class.
		/// </summary>
		public AboutForm(IServiceProvider serviceProvider) : this()
		{
			_serviceProvider = serviceProvider;
			Icon = MainForm.Icon;
		}

		/// <summary>
		/// Gets the version of this application from it's main assembly.
		/// </summary>
		public static string AssemblyVersion
		{
			[DebuggerStepThrough]
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		/// <summary>
		/// Opens the web site link when the user clicks on the link.
		/// </summary>
		private void WebSiteLinkClickedEventHandler(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(labelWebSite.Text);
		}

		#region Services

		/// <summary>
		/// Gets the <see cref="MainForm"/> service.
		/// </summary>
		public MainForm MainForm
		{
			[DebuggerStepThrough]
			get { return _serviceProvider.GetService(typeof(MainForm)) as MainForm; }
		}

		#endregion
	}
}

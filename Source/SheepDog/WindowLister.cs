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
using System.Collections.Generic;
using SheepDog.WindowsApi;

namespace SheepDog
{
	/// <summary>
	/// Utility class which can provide a list of visible top-level windows.
	/// </summary>
	public class WindowLister
	{
		private IList<Window> mWindows;

		/// <summary>
		/// Gets a list of all visible top-level windows.
		/// </summary>
		public IList<Window> GetVisibleWindows()
		{
			mWindows = new List<Window>();

			User32.EnumWindowsProc evalWindowCallback = EvalWindowCallback;
			User32.EnumWindows(evalWindowCallback, 0);

			return mWindows;
		}

		/// <summary>
		/// Callback that occurs for each window returned by the call to
		/// <see cref="User32.EnumWindows"/>.  Here, we filter out hidden
		/// (non-visible) windows, and add all other windows to the list
		/// that is returned from the <see cref="GetVisibleWindows"/> method.
		/// </summary>
		private bool EvalWindowCallback(IntPtr hWnd, int lParam)
		{
			if (User32.IsWindowVisible(hWnd) == false)
			{
				return true;
			}

			mWindows.Add(new Window(hWnd));

			return true;
		}
	}
}

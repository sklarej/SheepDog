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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SheepDog
{
	/// <summary>
	/// Utility class which will contains the main logic for repositioning 
	/// visible top top-level windows that are currently off-screen.
	/// </summary>
	public static class OffScreenWindowRepositioner
	{
		/// <summary>
		/// Number of pixels that a window is allowed to be offscreen before
		/// this class consider it for repositioning.  This prevents repositioning
		/// of windows that are right over the edge of the screen (ones that the
		/// user wouldn't have a problem repositioning if they wanted to.
		/// </summary>
		const int OffscreenMargin = 10;

		/// <summary>
		/// Repositions all visible top-level windows that are currently
		/// off-screen to a position that is visible to the user.
		/// </summary>
		public static int RepositionWindows()
		{
			int repositionedWindowCount = 0;
			WindowLister windowLister = new WindowLister();
			IList<Window> windows = windowLister.GetVisibleWindows();

			foreach (var window in windows)
			{
				if ((window.IsMaximized == false) && (IsWindowOffScreen(window)))
				{
					Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, "Repositioning window {0:X8} : {1}", window.Handle.ToInt32(), window.Title));

					window.SetPosition(Point.Empty);
					repositionedWindowCount++;
				}
			}

			return repositionedWindowCount;
		}

		/// <summary>
		/// Indicates whether a portion of the specified window resides off-screen.
		/// </summary>
		/// <remarks>
		/// This logic works fine for 1 screen, and for multi-monitor setups where
		/// the screens are in a rectangular setup.  It can be improved for 
		/// multi-monitor configurations where the screens are offset from each other.
		/// </remarks>
		public static Boolean IsWindowOffScreen(Window window)
		{
			Rectangle windowRectangle = window.ScreenRectangle;

			if ((windowRectangle.X == -32000) && (windowRectangle.Y == -32000))
			{
				return false;
			}

			Rectangle visibleArea = new Rectangle();

			foreach (Screen screen in Screen.AllScreens)
			{
				visibleArea = Rectangle.Union(visibleArea, screen.Bounds);
			}

			if (windowRectangle.Left < (visibleArea.Left - OffscreenMargin))
			{
				return true;
			}

			if (windowRectangle.Top < (visibleArea.Top - OffscreenMargin))
			{
				return true;
			}

			if (windowRectangle.Right > (visibleArea.Right + OffscreenMargin))
			{
				return true;
			}

			if (windowRectangle.Bottom > (visibleArea.Bottom + OffscreenMargin))
			{
				return true;
			}

			return false;
		}
	}
}

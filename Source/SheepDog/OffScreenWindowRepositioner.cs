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
        /// Number of pixels that a window is allowed to be off-screen before
        /// this class consider it for repositioning.  This prevents repositioning
        /// of windows that are right over the edge of the screen (ones that the
        /// user wouldn't have a problem repositioning if they wanted to.
        /// </summary>
        private const int OffscreenMargin = 10;

        /// <summary>
        /// Repositions all visible top-level windows that are currently
        /// off-screen to a position that is visible to the user.
        /// </summary>
        public static int RepositionWindows()
        {
            return RepositionWindowsOutsideOfScreens(Point.Empty);
        }

        private static int RepositionWindowsOutsideOfScreens(Point windowRepositionLocation)
        {
            WindowLister windowLister = new WindowLister();
            IList<Window> windows = windowLister.GetVisibleWindows();

            int repositionedWindowCount = 0;

            foreach (var window in windows)
            {
                if ((window.IsMaximized == false) && (IsWindowOffScreen(window)))
                {
                    Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, "Repositioning window {0:X8} : {1}", window.Handle.ToInt32(), window.Title));

                    window.SetPosition(windowRepositionLocation);
                    repositionedWindowCount++;
                }
            }

            return repositionedWindowCount;
        }

        /// <summary>
        /// Indicates whether a portion of the specified window resides off-screen.
        /// </summary>
        public static Boolean IsWindowOffScreen(Window window)
        {
            Rectangle windowRectangle = window.ScreenRectangle;

            //Ignore certain hidden windows
            if ((windowRectangle.X == -32000) && (windowRectangle.Y == -32000))
            {
                return false;
            }

            //Get the rectangle for the area of the window to test.  We reduce the size of the
            //window's area to test, so that we don't reposition windows that are only slightly
            //offscreen.
            Rectangle testRectangle = windowRectangle;
            testRectangle.Inflate(-OffscreenMargin, -OffscreenMargin);

            using (Region offScreenRegion = GetOffScreenRegion())
            {
                //Return whether any portion of the window was off-screen
                return offScreenRegion.IsVisible(testRectangle);
            }
        }

        /// <summary>
        /// Gets a <see cref="Region"/> that represents all off-screen area.
        /// </summary>
        private static Region GetOffScreenRegion()
        {
            Region region = new Region();
            region.MakeInfinite();

            foreach (Screen screen in Screen.AllScreens)
            {
                region.Exclude(screen.Bounds);
            }

            return region;
        }
    }
}

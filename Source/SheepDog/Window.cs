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
using System.Drawing;
using System.Text;
using SheepDog.WindowsApi;

namespace SheepDog
{
    /// <summary>
    /// Represents a top level window that is currently on the desktop.
    /// </summary>
    public class Window
    {
        private const int MaxStringLength = 256;

        private readonly IntPtr _handle;

        /// <summary>
        /// Creates a new instance of the <see cref="Window"/> class.
        /// </summary>
        public Window(IntPtr handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Title of the window.
        /// </summary>
        public IntPtr Handle
        {
            [DebuggerStepThrough]
            get
            {
                return _handle;
            }
        }

        /// <summary>
        /// Title of the window.
        /// </summary>
        public String Title
        {
            get
            {
                StringBuilder title = new StringBuilder(MaxStringLength);
                User32.GetWindowText(_handle, title, MaxStringLength);
                return title.ToString();
            }
        }

        /// <summary>
        /// Indicates the area of the screen that this window resides in.
        /// </summary>
        public Rectangle ScreenRectangle
        {
            get
            {
                User32.RECT rectangle;

                if (User32.GetWindowRect(_handle, out rectangle) == false)
                {
                    return new Rectangle();
                }

                return rectangle;
            }
        }

        /// <summary>
        /// Indicates whether the window is maximized.
        /// </summary>
        public Boolean IsMaximized
        {
            get
            {
                Int32 style = User32.GetWindowLong(_handle, User32.GWL_STYLE);

                return ((style & User32.WS_MAXIMIZE) == User32.WS_MAXIMIZE);
            }
        }

        /// <summary>
        /// Sets the position of this window.
        /// </summary>
        public void SetPosition(Point position)
        {
            User32.SetWindowPos(
                _handle,
                IntPtr.Zero,
                position.X,
                position.Y,
                0,
                0,
                User32.SWP_SHOWWINDOW | User32.SWP_NOSIZE);
        }
    }
}

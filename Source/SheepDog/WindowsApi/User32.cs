#region MIT License
/*
MIT License
Copyright (c) 2008 Josh Sklare

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
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace SheepDog.WindowsApi
{
	/// <summary>
	/// Collection of imported methods and constants related to user32.dll.
	/// </summary>
	public static class User32
	{
		public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

		//Modifier flags for RegisterHotKey
		public const int MOD_ALT = 1;
		public const int MOD_CONTROL = 2;
		public const int MOD_SHIFT = 4;
		public const int MOD_WIN = 8;

		//Window style returned by GetWindowLong
		public const uint WS_MAXIMIZE = 0x01000000;

		//Window message that occurs when a hotkey combination is pressed
		public const int WM_HOTKEY = 0x312;

		//Flag for value to be retrieved by GetWindowLong
		public const int GWL_STYLE = -16;

		//Flags for SetWindowPos
		public const uint SWP_NOSIZE = 0x1;
		public const uint SWP_NOMOVE = 0x2;
		public const uint SWP_SHOWWINDOW = 0x40;

		[DllImport("user32", SetLastError = true)]
		public static extern Boolean RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

		[DllImport("user32", SetLastError = true)]
		public static extern Boolean UnregisterHotKey(IntPtr hwnd, int id);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);

		[DllImport("user32.dll")]
		public static extern int GetWindowModuleFileName(IntPtr hWnd, StringBuilder title, int size);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean EnumWindows(EnumWindowsProc ewp, int lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll")]
		[return : MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[Serializable, StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public static implicit operator Rectangle(RECT rect)
			{
				return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
			}

			public static implicit operator RECT(Rectangle rect)
			{
				return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
			}
		}
	}
}

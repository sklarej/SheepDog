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

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SheepDog
{
	/// <summary>
	/// Represents a global hotkey, storing it's key and modifier keys.
	/// </summary>
	/// <remarks>
	/// This class is needed because the framework <see cref="Keys"/> class does
	/// not have a modifier to represent the Windows key.
	/// </remarks>
	public class Hotkey
	{
		public Keys Key { get; set; }
		public Boolean Shift { get; set; }
		public Boolean Control { get; set; }
		public Boolean Alt { get; set; }
		public Boolean Windows { get; set; }

		/// <summary>
		/// Returns the hotkey as a user readable string.
		/// </summary>
		[DebuggerStepThrough]
		public override string ToString()
		{
			String output = "";

			if (Control)
			{
				output += "Ctrl+";
			}

			if (Alt)
			{
				output += "Alt+";
			}

			if (Shift)
			{
				output += "Shift+";
			}

			if (Windows)
			{
				output += "Win+";
			}

			output += Key.ToString();

			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		[DebuggerStepThrough]
		public override bool Equals(object obj)
		{
			Hotkey hotkey = obj as Hotkey;

			if (hotkey == null)
			{
				return false;
			}

			return (Control == hotkey.Control) &&
				(Alt == hotkey.Alt) &&
				(Shift == hotkey.Shift) &&
				(Windows == hotkey.Windows) &&
				(Key == hotkey.Key);
		}

		/// <summary>
		/// 
		/// </summary>
		[DebuggerStepThrough]
		public override int GetHashCode()
		{
			return Control.GetHashCode() ^ 
				Alt.GetHashCode() ^ 
				Shift.GetHashCode() ^ 
				Windows.GetHashCode() ^ 
				Key.GetHashCode();
		}
	}
}

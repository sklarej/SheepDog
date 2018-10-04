#region MIT License
/*
MIT License
Copyright (c) 2009-2018 Joshua Sklare

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

namespace SheepDog.WindowsApi
{
    /// <summary>
    /// Collection of helper methods that wrap hotkey related Windows API calls.
    /// </summary>
    public static class GlobalHotKeyInterop
    {
        /// <summary>
        /// Registers the specified hotkey as a global hotkey.
        /// </summary>
        public static bool RegisterGlobalHotkey(short hotkeyAtomId, IntPtr hWnd, Hotkey hotkey)
        {
            int modifiers = 0;

            if (hotkey.Shift)
            {
                modifiers |= User32.MOD_SHIFT;
            }

            if (hotkey.Control)
            {
                modifiers |= User32.MOD_CONTROL;
            }

            if (hotkey.Alt)
            {
                modifiers |= User32.MOD_ALT;
            }

            if (hotkey.Windows)
            {
                modifiers |= User32.MOD_WIN;
            }

            return User32.RegisterHotKey(hWnd, hotkeyAtomId, modifiers, (int) hotkey.Key);
        }

        /// <summary>
        /// Unregisters the global hotkey for the specified form.
        /// </summary>
        public static void UnregisterGlobalHotkey(short hotkeyAtomId, IntPtr hWnd)
        {
            User32.UnregisterHotKey(hWnd, hotkeyAtomId);
        }

        /// <summary>
        /// Returns whether the specified hotkey is available to be registered.
        /// </summary>
        public static Boolean IsHotkeyAvailable(Hotkey hotkey)
        {
            short atom = Kernel32.GlobalAddAtom("HotkeyAvailableTestAtom-" + Process.GetCurrentProcess().Id);
            Boolean hotkeyRegistered = RegisterGlobalHotkey(atom, IntPtr.Zero, hotkey);

            if (hotkeyRegistered)
            {
                UnregisterGlobalHotkey(atom, IntPtr.Zero);
            }

            return hotkeyRegistered;
        }
    }
}

#region MIT License
/*
MIT License
Copyright (c) 2009-2018 Joshua Sklare
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

namespace SheepDog
{
    /// <summary>
    /// Contains all the logic for running the application as a command line
    /// program.
    /// </summary>
    public class CommandLineController
    {
        private readonly CommandLineParser _commandLineParser;

        /// <summary>
        /// Creates a new instance of the <see cref="CommandLineController"/> class.
        /// </summary>
        /// <param name="arguments">Arguments that were passed to the application.</param>
        public CommandLineController(String[] arguments)
        {
            _commandLineParser = new CommandLineParser(arguments);
        }

        /// <summary>
        /// Attempts to run the application as a command line application.  If it
        /// is supposed to run in normal (system tray) mode, then this does not do
        /// anything and returns false.  It returns true if the application was run
        /// in command line mode.
        /// </summary>
        public Boolean Run()
        {
            if (_commandLineParser.RepositionNow)
            {
                OffScreenWindowRepositioner.RepositionWindows();
                return true;
            }

            return false;
        }
    }
}

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
using System.Diagnostics;
using System.Threading;

namespace SheepDog
{
    /// <summary>
    /// Class which can be used to ensure that only a single instance of a program is running.
    /// </summary>
    /// <example>
    /// This class is designed to be used in a "using" statement, where the main logic of the
    /// program will be contained within that statement.  An example of it's usage is:
    /// <code>
    /// using (SingleInstanceController singleInstance = new SingleInstanceController("ApplicationName"))
    /// {
    ///     if (singleInstance.IsOnlyInstance == false)
    ///     {
    ///         return;
    ///     }
    ///
    ///     ...
    ///     Main program logic
    ///     ...
    /// }
    /// </code>
    /// </example>
    public class SingleInstanceController : IDisposable
    {
        private const String SingleInstanceMutextPrefix = "SingleInstanceMutex-";

        private Mutex _singleInstanceMutex;
        private readonly Boolean _isOnlyInstance;

        /// <summary>
        /// Creates a new instance of the <see cref="SingleInstanceController"/> class.
        /// </summary>
        /// <param name="applicationUniqueId">
        /// Identifier that will be used to identify instances of the running application.
        /// This can be the name of the application.
        /// </param>
        public SingleInstanceController(String applicationUniqueId)
        {
            _singleInstanceMutex = new Mutex(true, SingleInstanceMutextPrefix + applicationUniqueId, out _isOnlyInstance);
        }

        /// <summary>
        /// Returns whether this was the only running instance of this application at the
        /// time this <see cref="SingleInstanceController"/> was created.
        /// </summary>
        public Boolean IsOnlyInstance
        {
            [DebuggerStepThrough]
            get { return _isOnlyInstance; }
        }

        /// <summary>
        /// Releases the mutex with is used to test that a single instance is running.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the mutex with is used to test that a single instance is running.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether managed resources should be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_singleInstanceMutex != null)
                {
                    _singleInstanceMutex.Close();
                    _singleInstanceMutex = null;
                }
            }
        }
    }
}

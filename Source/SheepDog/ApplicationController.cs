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
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms;

namespace SheepDog
{
    /// <summary>
    /// Creates all the major pieces of the application, places them in a 
    /// service container so they can be accessed by other components, and 
    /// then initializes them for running.
    /// </summary>
    public class ApplicationController : IDisposable
    {
        private ServiceContainer _serviceContainer;

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        public ApplicationController()
        {
            _serviceContainer = new ServiceContainer();
        }

        /// <summary>
        /// Initiates the running of the application.
        /// </summary>
        public void Run()
        {
            using (SingleInstanceController singleInstance = new SingleInstanceController(Application.ProductName))
            {
                if (singleInstance.IsOnlyInstance == false)
                {
                    Trace.WriteLine("Another running instance of this application has been detected.");
                    return;
                }

                InitializeServices();

                Application.Run(MainForm);
            }
        }

        /// <summary>
        /// Creates all the services needed to run the application, adds them to the
        /// service container, and then initializes them.
        /// </summary>
        private void InitializeServices()
        {
            //Create all services and add them to the service container
            MainForm mainForm = new MainForm(_serviceContainer);
            _serviceContainer.AddService(typeof(MainForm), mainForm);

            GlobalHotkeyService hotKeyService = new GlobalHotkeyService(_serviceContainer);
            _serviceContainer.AddService(typeof(GlobalHotkeyService), hotKeyService);

            ConfigurationService configurationService = new ConfigurationService(_serviceContainer);
            _serviceContainer.AddService(typeof(ConfigurationService), configurationService);

            //Now that all services have been added to the service container, they can be initialized
            configurationService.Initialize();
            hotKeyService.Initialize();
            mainForm.Initialize();
        }

        /// <summary>
        /// Disposes of the service container contained in this controller.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the service container contained in this controller.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether managed resources should be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_serviceContainer != null)
                {
                    _serviceContainer.Dispose();
                    _serviceContainer = null;
                }
            }
        }

        #region Services

        /// <summary>
        /// Gets the main form for the application.
        /// </summary>
        private MainForm MainForm
        {
            [DebuggerStepThrough]
            get { return _serviceContainer.GetService(typeof (MainForm)) as MainForm; }
        }

        #endregion
    }
}

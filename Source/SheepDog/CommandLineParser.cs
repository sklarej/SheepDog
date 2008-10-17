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

namespace SheepDog
{
	/// <summary>
	/// Class which parses the command line arguments that are passed to this
	/// application.
	/// </summary>
	public class CommandLineParser
	{
		private const String RepositionNowArgument = "/RepositionNow";

		private readonly Boolean _repositionNow;

		/// <summary>
		/// Creates a new instance of the <see cref="CommandLineParser"/> class.
		/// </summary>
		/// <param name="arguments">Arguments that were passed to the application.</param>
		public CommandLineParser(String[] arguments)
		{
			foreach (String argument in arguments)
			{
				if (String.Equals(argument, RepositionNowArgument, StringComparison.InvariantCultureIgnoreCase))
				{
					_repositionNow = true;
				}
			}
		}

		/// <summary>
		/// Indicates if the "/RepositionNow" argument was passed as a command line
		/// argument to the application.
		/// </summary>
		public Boolean RepositionNow
		{
			get
			{
				return _repositionNow;
			}
		}
	}
}

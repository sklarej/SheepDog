SheepDog Beta 0.8 Readme
October 17, 2008
http://www.codeplex.com/SheepDog

===============================================================================
DESCRIPTION

SheepDog is a free, open-source utility for repositioning off-screen windows. 

SheepDog is useful whenever you have an application or window that you can't 
access because it is located off your visible screen.

Off-screen windows can occur in a variety of situations, including:

    * Switching between monitors on a laptop
    * Multiple-screen setup
    

===============================================================================
HOW TO USE

NORMAL MODE (SYSTEM TRAY):

SheepDog runs in the Windows system tray and can be activated in two ways:

    * Choosing the "Reposition Windows" menu option after right clicking on 
      SheepDog in the system tray.
    * Pressing a hotkey combination. The hotkey defaults to Win+W, but can 
      be changed in the "Options" menu.

When activated, SheepDog will re-position the off-screen all windows back 
onto your main screen.

COMMAND LINE MODE:

SheepDog can also be run as a command line program.  This allows you to 
create a shortcut to reposition windows, run it from a command prompt,
or incorporate it in an application launcher program.  When run in this mode,
SheepDog will simply close after repositioning off-screen windows.

To run SheepDog in this way, simply execute it with the "/RepositionNow"
parameter:

    SheepDog /RepositionNow
    
    
===============================================================================
HOW TO INSTALL

Currently there is no setup program for SheepDog, although there will be one in
the near future.  However, SheepDog can simply be copied onto your computer and
run without needing to install it.

If you want SheepDog to always run, you will have to create a shortcut to it in
your Windows startup folder.


===============================================================================
SYSTEM REQUIREMENTS

SheepDog is designed to work on Windows 2000 and later, although it's only been 
tested on Windows XP and Vista so far.  It works on both 32-bit and 64-bit 
versions of Windows.

The .NET Framework 2.0 is also required to run SheepDog.


===============================================================================
LICENSING

SheepDog has been released under the MIT License.  All the source code is 
available for download at:
  
  http://www.codeplex.com/SheepDog

In a nutshell, the MIT License permits you to download and use the code however
you like.  You can use the source code, either modified or in it's original form,
in whatever project you want (even in commercial applications).
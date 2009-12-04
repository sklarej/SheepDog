SheepDog 1.0 Readme
December 4, 2009
http://www.codeplex.com/SheepDog

===============================================================================
DESCRIPTION

SheepDog is a free, open-source utility for repositioning off-screen windows. 

SheepDog is useful whenever you have an application or window that you can't 
access because it is located off your visible screen.

Off-screen windows can occur in a variety of situations, including:

    * Switching between monitors on a laptop
    * Computers with multiple monitors
    * Applications that save an invalid startup position
    

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
SheepDog will close after repositioning off-screen windows.

To run SheepDog in this way, execute it with the "/RepositionNow" parameter:

    SheepDog /RepositionNow
    
    
===============================================================================
HOW TO INSTALL

There are 2 ways to install SheepDog:

1) Install SheepDog using the SheepDogSetup.msi setup package.  This will 
   place SheepDog in your Program Files directory and set it to run on startup.

2) Copy SheepDog.exe onto your computer without installing it.  A zip file 
   containing SheepDog is available for download. (If you wish for SheepDog to 
   always run, you can create a shortcut to it in your Windows startup folder.)


===============================================================================
SYSTEM REQUIREMENTS

* Windows 2000, Windows XP, Windows Vista, Windows 7, Windows Server 2003,
  or Windows Server 2008 (32-bit and 64-bit for all versions)

* Microsoft .NET Framework 2.0 or greater


===============================================================================
LICENSING

SheepDog has been released under the MIT License.  All the source code is 
available for download at:
  
  http://www.codeplex.com/SheepDog

In a nutshell, the MIT License permits you to download and use the code however
you like.  You can use the source code, either modified or in it's original form,
in whatever project you want (even in commercial applications).
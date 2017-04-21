# DriverInstaller
A simple C# class that let's you check for a specific driver and install one if it's missing

Prerequisities:
  - You have to have a driver installer EXECUTABLE in the same directory (as a default) as your app

Contents:
  - DriverInstall.cs - the actual class to add to your project
  - AutoElevate - a code snippet described below

In case the driver to be installed needs administrator privileges, and it is not preferred to run the application as an administrator
every time it is run, you can include the "AutoElevate" code snippet into a method that is run first when the app is started. The code snippet in question automatically elevates the user to administrator level (a prompt of course pops up) and restarts the application. This happens only the first time the application is run.

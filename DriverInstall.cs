using System;
using System.Diagnostics;
using System.Management;

namespace DriverInstall
{
    class Driver
    {
        // A class to add to your project with which you can scan the system for a specific device- or system driver
        // and install it in case it's missing from the target computer.
        // The prerequisites are that you have a driver installer EXECUTABLE in the same folder as your app is located

        // <requestedExecutionLevel level="requireAdministrator" uiAccess="false" /> change this in application manifest
        // if you get "Access denied" error when trying to run the installer as administrator
        
        // In some cases, you may have to manually add a reference to the System.Management namespace

        public void DriverCheck(string driverName)
        {
            bool driverFound = false;
            string s = "";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SystemDriver ");
            // optionally search for "DeviceName" (instead of "Name") from Win32_PnPSignedDriver


            foreach (ManagementObject obj in searcher.Get())
            {
                try
                {

                    s = string.IsNullOrEmpty(obj.GetPropertyValue("Name").ToString()) ? string.Empty : obj.GetPropertyValue("Name").ToString();

                    //you can write the drivers here for ex. to console, if you don't know exactly what you're looking for

                    if (s.Contains(driverName)) // desired driver name
                    {
                        driverFound = true;
                    }
                }
                catch (Exception e)
                {
                    Debug.Write(e.Message);
                }


            }


            
            if (!driverFound)
            {
                try
                {
                    
                    Process pProcess = new Process();
                    pProcess.StartInfo.FileName = Environment.CurrentDirectory + @"/DriverName.exe"; // your driver executable, located in the same directory as your application
                    pProcess.StartInfo.UseShellExecute = false;
                    pProcess.StartInfo.RedirectStandardOutput = true;
                    pProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pProcess.StartInfo.CreateNoWindow = true; //do not display a window
                    pProcess.StartInfo.Verb = "runas"; // run as administrator
                    pProcess.Start();

                    string output = pProcess.StandardOutput.ReadToEnd(); //The output result (optional)
                    pProcess.WaitForExit();
                }
                catch (Exception e)
                {
                    Debug.Write(e.Message);
                }
            }

        }
    }
}

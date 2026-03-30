using System.Diagnostics;

public static class AppService
{
    public static void StopApp(string appName)
    {
        try
        {
            var processes = Process.GetProcessesByName(appName); // Create an array of type Process that represents the process resources running the specified appName
            foreach (var process in processes)
            {
                process.Kill(true);
            }
            //EmailService.SendResultLAN("App Service", "Stopped app: " + appName);
        }
        catch (Exception ex)
        {
            //EmailService.SendResultLAN("App Service", "Error stopping app: " + appName + " - " + ex.Message);
        }
    }
    public static void StartApp(string appName)
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives(); // Get all disk drives
        appName = appName + ".exe"; // Ensure the app name has .exe extension
        foreach (var drive in allDrives)
        {
            if (!drive.IsReady || drive.DriveType != DriveType.Fixed) continue; // Skip if drive is not ready, or not a fixed drive
            try
            {
                var options = new EnumerationOptions
                {
                    RecurseSubdirectories = true, // Search in subdirectories
                    IgnoreInaccessible = true, // Ignore inaccessible directories
                    MatchCasing = MatchCasing.CaseInsensitive
                };
                var filePath = Directory.EnumerateFiles(drive.RootDirectory.FullName, appName, options).FirstOrDefault(); // Find and return the first found file
                if (filePath != null)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true, // Use shell to start the process
                            Verb = "runas" // Run as administrator
                        });
                        //EmailService.SendResultLAN("Find App Service", "App started: " + filePath);
                    }
                    catch (Exception ex)
                    {
                        //EmailService.SendResultLAN("Find App Service", "Error accessing file: " + filePath + " - " + ex.Message);
                    }
                }
                else
                {
                    //EmailService.SendResultLAN("Find App Service", "App not found: " + appName);
                }
            }
            catch (Exception ex)
            {
                //EmailService.SendResultLAN("Find App Service", "Error accessing drive: " + drive.Name + " - " + ex.Message);
                continue;
            }
        }
    }
}
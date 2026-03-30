using System.Diagnostics;

public static class AppService
{
        public static async Task ListAllApps()
    {
        var apps = Process.GetProcesses().Where(p =>
    {
        try
        {
            return p.MainWindowHandle != IntPtr.Zero &&
                   !string.IsNullOrEmpty(p.MainWindowTitle);
        }
        catch
        {
            return false;
        }
    });
        string appList = string.Join("\n", apps.Select(p => $"{p.ProcessName} (ID: {p.Id})"));
        await FileService.MadeTextFile(appList);
    }
    public static async Task StopApp(string appName)
    {
        try
        {
            var processes = Process.GetProcessesByName(appName); 
            foreach (var process in processes)
            {
                process.Kill(true);
            }
            await RespondService.SendMessageToWeb("App stopped: " + appName);
        }
        catch (Exception ex)
        {
            await RespondService.SendMessageToWeb("Error stopping app: " + appName + " - " + ex.Message);
        }
    }
    public static async Task StartApp(string appName)
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives(); 
        appName = appName + ".exe"; 
        foreach (var drive in allDrives)
        {
            if (!drive.IsReady || drive.DriveType != DriveType.Fixed) continue; 
            try
            {
                var options = new EnumerationOptions
                {
                    RecurseSubdirectories = true, 
                    IgnoreInaccessible = true, 
                    MatchCasing = MatchCasing.CaseInsensitive
                };
                var filePath = Directory.EnumerateFiles(drive.RootDirectory.FullName, appName, options).FirstOrDefault(); 
                if (filePath != null)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true, 
                            Verb = "runas" 
                        });
                        await RespondService.SendMessageToWeb("App started: " + filePath);
                        return;
                    }
                    catch (Exception ex)
                    {
                        await RespondService.SendMessageToWeb("Error starting app: " + filePath + " - " + ex.Message);
                    }
                }
                else
                {
                    await RespondService.SendMessageToWeb("App not found: " + appName);
                }
            }
            catch (Exception ex)
            {
                await RespondService.SendMessageToWeb("Error accessing drive: " + drive.Name + " - " + ex.Message);
                continue;
            }
        }
    }
}
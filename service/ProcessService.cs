using System.Diagnostics;
public static class ProcessService
{
    public static void ListAllProcess()
    {
        var processes = Process.GetProcesses();
        //EmailService.SendResultLAN("Process Service", "Listing all processes: " + string.Join("\n", processes.Select(p => $"{p.ProcessName} (ID: {p.Id})")));
    }
    public static void KillProcessByName(string processName)
    {
        try
        {
            var processes = Process.GetProcessesByName(processName); // Create an array of type Process that represents the process resources running the specified appName
            if(processes.Length == 0)
            {
                //EmailService.SendResultLAN("Process Service", "No process found with name: " + processName);
                return;
            }
            foreach (var process in processes)
            {
                try
                {
                    process.Kill(true); // Kill the process and its children
                }
                catch (Exception ex)
                {
                    //EmailService.SendResultLAN("Process Service", "Error while trying to kill process: " + ex.Message);
                }
            }
            //EmailService.SendResultLAN("Process Service", "Stopped process: " + processName);
        }
        catch(Exception ex)
        {
            //EmailService.SendResultLAN("Process Service", "Error while trying to kill process: " + ex.Message);
            return;
        }
    }
    public static void StartProcessByName(string processName)
    {
        try
        {
            AppService.StartApp(processName);
        }
        catch (Exception ex)
        {
            //EmailService.SendResultLAN("Process Service", "Error while trying to start process: " + ex.Message);
        }
    }

}
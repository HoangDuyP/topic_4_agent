using System.Diagnostics;
public static class ProcessService
{
    public static async Task ListAllProcess()
    {
        var processes = Process.GetProcesses();
        string processList = string.Join("\n", processes.Select(p => $"{p.ProcessName} (ID: {p.Id})"));
        await FileService.MadeTextFile(processList);
    }
    public static async Task KillProcessByName(string processName)
    {
        try
        {
            var processes = Process.GetProcessesByName(processName); 
            if(processes.Length == 0)
            {
                await RespondService.SendMessageToWeb("Process not found: " + processName);
                return;
            }
            foreach (var process in processes)
            {
                try
                {
                    process.Kill(true); 
                }
                catch (Exception ex)
                {
                    await RespondService.SendMessageToWeb("Error while trying to kill process: " + ex.Message);
                }
            }
            await RespondService.SendMessageToWeb("Process stopped: " + processName);
        }
        catch(Exception ex)
        {
            await RespondService.SendMessageToWeb("Error while trying to kill process: " + ex.Message);
            return;
        }
    }
    public static async Task StartProcessByName(string processName)
    {
        try
        {
            await AppService.StartApp(processName);
        }
        catch (Exception ex)
        {
            await RespondService.SendMessageToWeb("Error while trying to start process: " + ex.Message);
        }
    }

}
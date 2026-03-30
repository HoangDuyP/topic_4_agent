using System.Diagnostics;
public static class ShutdownService
{
    public static async Task Shutdown()
    {
        Process.Start("shutdown", "/s /f /t 0"); // force shutdown /s = shutdown /f = force /t 0 = no timer
        await RespondService.SendMessageToWeb("The system is shutting down.");
    }
    public static async Task Restart()
    {
        Process.Start("shutdown", "/r /f /t 0"); // force restart /r = restart /f = force /t 0 = no timer
        await RespondService.SendMessageToWeb("The system is restarting.");
    }
}
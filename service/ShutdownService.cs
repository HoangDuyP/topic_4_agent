using System.Diagnostics;
public static class ShutdownService
{
    public static void Shutdown()
    {
        Process.Start("shutdown", "/s /f /t 0"); // force shutdown /s = shutdown /f = force /t 0 = no timer
        //EmailService.SendResultLAN("Shutdown Service", "The system is shutting down.");
    }
    public static void Restart()
    {
        Process.Start("shutdown", "/r /f /t 0"); // force restart /r = restart /f = force /t 0 = no timer
        //EmailService.SendResultLAN("Shutdown Service", "The system is restarting.");
    }
}
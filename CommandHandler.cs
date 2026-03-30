public static class CommandHandler
{
    public static async void HandleCommand(string command)
    {
        try
        {
            command = command.Trim().ToLower(); // Remove accidental white space and cap lock
            if (command == "list process" || command == "list applications")
            {
                ProcessService.ListAllProcess();
            }
            else if (command.StartsWith("startprocess "))
            {
                string processName = command.Substring(13).Trim(); // get process name from command, remove the startprocess_ prefix
                if (!string.IsNullOrEmpty(processName))
                {
                    ProcessService.StartProcessByName(processName);
                }
                else
                {
                    //EmailService.SendResultLAN("Command Error", "Process name cannot be empty.");
                }
            }
            else if (command.StartsWith("stopprocess "))
            {
                string processName = command.Substring(12).Trim(); // get process name from command, remove the stopprocess_ prefix
                if (!string.IsNullOrEmpty(processName))
                {
                    ProcessService.KillProcessByName(processName);
                }
                else
                {
                    //EmailService.SendResultLAN("Command Error", "Process name cannot be empty.");
                }
            }
            else if (command.StartsWith("startapp "))
            {
                string appName = command.Substring(9).Trim(); // get app name from command, remove the startapp_ prefix
                if (!string.IsNullOrEmpty(appName))
                {
                    AppService.StartApp(appName);
                }
                else
                {
                    //EmailService.SendResultLAN("Command Error", "App name cannot be empty.");
                }
            }
            else if (command.StartsWith("stopapp "))
            {
                string appName = command.Substring(8).Trim(); // get app name from command, remove the startapp_ prefix
                if (!string.IsNullOrEmpty(appName))
                {
                    AppService.StopApp(appName);
                }
                else
                {
                    //EmailService.SendResultLAN("Command Error", "App name cannot be empty.");
                }
            }
            else if (command == "screenshot")
            {
                await ScreenshotService.CaptureScreenshot();
            }
            else if (command == "shutdown")
            {
                ShutdownService.Shutdown();
            }
            else if (command == "restart")
            {
                ShutdownService.Restart();
            }
            else if (command.StartsWith("get "))
            {
                string fileName = command.Substring(4).Trim(); // get file name from command, remove the get_ prefix
                if (!string.IsNullOrEmpty(fileName))
                {
                    FileService.GetFile(fileName);
                }
                else
                {
                    //EmailService.SendResultLAN("Command Error", "File name cannot be empty.");
                }
            }
            else
            {
                //EmailService.SendResultLAN("Command Error", "Unknown command: " + command);
            }
        }
        catch (Exception ex)
        {
            //EmailService.SendResultLAN("Command Error", "An error occurred while processing the command: " + ex.Message);
        }
    }
}
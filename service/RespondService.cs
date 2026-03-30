using Microsoft.AspNetCore.SignalR.Client;
public static class RespondService
{
    public static HubConnection? Connection;
     public static async Task SendFileToWeb(string fileName, string fileContent)
    {     if (Connection == null)
        {
            Console.WriteLine("Not connected to Hub");
            return;
        }
        try{
            await Connection.InvokeAsync("ReceiveFileFromAgent", fileName, fileContent);
            Console.WriteLine($"Sent file to web: {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send file: {ex.Message}");
        }
    }
    public static async Task SendMessageToWeb(string message)
    {
        if (Connection == null)
        {
            Console.WriteLine("Not connected to Hub");
            return;
        }
        try
        {
            await Connection.InvokeAsync("SendMessageToWeb", message);
            Console.WriteLine($"Sent message to web: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send message: {ex.Message}");
        }
    }
}
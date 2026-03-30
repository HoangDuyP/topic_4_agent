public static class FileService
{
    public static async Task GetFile(string fileName)
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives(); 
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
                var filePath = Directory.EnumerateFiles(drive.RootDirectory.FullName, fileName, options).FirstOrDefault();
                if (filePath != null)
                {
                    Console.WriteLine($"File found: {filePath}");
                    var fileInfo = new FileInfo(filePath);
                    var bytes = File.ReadAllBytes(fileInfo.FullName);
                    var base64 = Convert.ToBase64String(bytes);
                    await RespondService.SendFileToWeb(fileInfo.Name, base64);
                    return;
                }
                else
                {
                    await RespondService.SendMessageToWeb("File not found: " + fileName);
                }
            }
            catch (Exception ex)
            {                Console.WriteLine($"Error accessing drive {drive.Name}: {ex.Message}");
                continue;
            }
        }
    }
    public static async Task GetFileByPath(string path)
    {
        try
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                var bytes = File.ReadAllBytes(fileInfo.FullName);
                var base64 = Convert.ToBase64String(bytes);
                await RespondService.SendFileToWeb(fileInfo.Name, base64);
            }
            else
            {
                await RespondService.SendMessageToWeb("File not found: " + fileInfo.Name);
            }
        }
        catch (Exception ex)
        {
            await RespondService.SendMessageToWeb("Error accessing file: " + path + " - " + ex.Message);
        }
    }
    public static async Task MadeTextFile(string content)
    {
        string filePath = Path.Combine(Path.GetTempPath(), "output.txt");
        File.WriteAllText(filePath, content);
        var fileInfo = new FileInfo(filePath);
        var bytes = File.ReadAllBytes(fileInfo.FullName); 
        var base64 = Convert.ToBase64String(bytes);
        await RespondService.SendFileToWeb(fileInfo.Name, base64);
    }
}
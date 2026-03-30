using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://semiprotected-aubrey-undevelopmentally.ngrok-free.dev/hub")
    .WithAutomaticReconnect()
    .Build();

try
{
    connection.On<string>("ReceiveCommand", async (msg) =>
    {
        Console.WriteLine(msg);
        if(msg == "PING FROM WEB")
        {
            Console.WriteLine("Received PING, sending PONG...");
            await connection.InvokeAsync("SendMessageToWeb", "PONG FROM AGENT");
        }
    });
    await connection.StartAsync();
    Console.WriteLine("Connected to Hub");

    // 🔹 gọi Hub để đăng ký agent
    await connection.InvokeAsync("RegisterAgent");
}
catch (Exception ex)
{
    Console.WriteLine($"Connection failed: {ex.Message}");
}

Console.ReadLine();
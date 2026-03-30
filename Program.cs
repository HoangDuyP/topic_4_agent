using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://semiprotected-aubrey-undevelopmentally.ngrok-free.dev/hub")
    .WithAutomaticReconnect()
    .Build();

try
{
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
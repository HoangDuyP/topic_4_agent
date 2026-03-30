using Microsoft.AspNetCore.SignalR.Client;
using System.Net;
using System.Net.Sockets;

var connection = new HubConnectionBuilder()
    .WithUrl("https://semiprotected-aubrey-undevelopmentally.ngrok-free.dev/hub")
    .WithAutomaticReconnect()
    .Build();

try
{
    await connection.StartAsync();
    Console.WriteLine("Connected to Hub");
    var ip = Dns.GetHostEntry(Dns.GetHostName())
        .AddressList
        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)
        ?.ToString();

    Console.WriteLine("IP: " + ip);
    await connection.InvokeAsync("SendMessage", ip);
}
catch (Exception ex)
{
    Console.WriteLine($"Connection failed: {ex.Message}");
}

Console.ReadLine();
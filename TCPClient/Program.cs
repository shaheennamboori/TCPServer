using System.Net.Sockets;
using System.Text;

string serverIP = "127.0.0.1"; // Replace with the public IP of the server
int port = 13000; // Replace with the server's port

try
{
    // Connect to the server
    using TcpClient client = new(serverIP, port);
    Console.WriteLine("Connected to server!");

    using NetworkStream stream = client.GetStream();
    Console.WriteLine("Type messages. Press 'Enter' to send and 'Ctrl+C' to exit.");

    while (true)
    {
        // Read keystrokes
        string message = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(message))
        {
            // Convert message to bytes and send to the server
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            Console.WriteLine($"Sent: {message}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

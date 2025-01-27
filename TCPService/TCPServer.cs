using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPService
{
    internal class TCPServer
    {
        private TcpListener _listener;
        public TCPServer()
        {
            StartServer();
        }

        private void StartServer()
        {
            var port = 13000;
            var hostAddress = IPAddress.Parse("127.0.0.1");

            _listener = new TcpListener(hostAddress, port);
            _listener.Start();

            byte[] buffer = new byte[1024];
            string recievedMessage;

            using TcpClient client = _listener.AcceptTcpClient();
            var tcpStream = client.GetStream();
            int streamTotal;
            StringBuilder messageBuilder = new();
            var response = "Connected Successfully";
            bool responseAck = false;

            while ((streamTotal = tcpStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                if (!responseAck)
                {
                    tcpStream.Write(Encoding.UTF8.GetBytes(response, 0, response.Length));
                    responseAck = true;
                }
                
                recievedMessage = Encoding.UTF8.GetString(buffer, 0, streamTotal);
                Console.WriteLine($"Captured part of the conversation: {recievedMessage}\n");
                messageBuilder.Append(recievedMessage);
            }

            Console.WriteLine($"Conversation:\n\n{messageBuilder}");
        }
    }
}

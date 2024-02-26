using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    internal class Server
    {
        private IPEndPoint IpEndPoint;

        public Server(IPEndPoint ipEndPoint)
        {
            this.IpEndPoint = ipEndPoint;
        }

        public async Task StartServerAsync()
        {

            using Socket listener = new(IpEndPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);

            listener.Bind(IpEndPoint);
            listener.Listen(100);

            await Console.Out.WriteLineAsync($"Listening on... {IpEndPoint}");

            Socket handler = await listener.AcceptAsync();
            while (true)
            {
                // Receive message.
                byte[] buffer = new byte[1_024];
                int received = await handler.ReceiveAsync(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, received);

                string eom = "<|EOM|>";
                if (response.IndexOf(eom) > -1 /* is end of message */)
                {
                    Console.WriteLine(
                        $"Socket server received message: \"{response.Replace(eom, "")}\"");

                    string ackMessage = "<|ACK|>";
                    byte[] echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                    await handler.SendAsync(echoBytes, 0);
                    Console.WriteLine(
                        $"Socket server sent acknowledgment: \"{ackMessage}\"");

                    break;
                }
                // Sample output:
                //    Socket server received message: "Hi friends 👋!"
                //    Socket server sent acknowledgment: "<|ACK|>"
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    internal class Client
    {
        private IPEndPoint ipEndPoint;

        public Client(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;
            while (true)
            {
            string msg = CreateMessage();
             _= StartClientAsync(msg + "!<|EOM|>");
            }
            
        }

        public async Task StartClientAsync(string message)
        {
            using Socket client = new(ipEndPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            while (true) { 

            await client.ConnectAsync(ipEndPoint);

            while (true)
            {
                // Send message.
                //string message = "Hi friends 👋!<|EOM|>";
               // string message = CreateMessage() + "!<|EOM|>";
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine($"Socket client sent message: \"{message}\"");

                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                if (response == "<|ACK|>")
                {
                    Console.WriteLine(
                        $"Socket client received acknowledgment: \"{response}\"");
                    break;
                }
                // Sample output:
                //     Socket client sent message: "Hi friends 👋!<|EOM|>"
                //     Socket client received acknowledgment: "<|ACK|>"
            }

            client.Shutdown(SocketShutdown.Both);
        }
    }

        private string? CreateMessage()
        {
            Console.WriteLine("MSG:");
            return Console.ReadLine();
        }
    }
}

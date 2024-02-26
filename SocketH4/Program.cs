using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IPEndPoint? ipEndPoint = IpClassLibrary.Class1.GetIPEndPoint();
            if (ipEndPoint != null)
            {
                var t = Task.Run(async () => await new Server(ipEndPoint).StartServerAsync());
                while (true) Thread.Sleep(1000);
            }
            else Console.WriteLine("No Ip EndPoint Found!!!");
        }
    }
}

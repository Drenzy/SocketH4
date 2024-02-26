using System.Net;

namespace SocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IPEndPoint? ipEndPoint = IpClassLibrary.Class1.GetIPEndPoint();
            if (ipEndPoint != null) {
                //var t = Task.Run(async () => await new Client(ipEndPoint).StartClientAsync());
               // var res = t;
               new Client(ipEndPoint);
                while (true)
                {
                    Thread.Sleep(1000);
                }
                
            }
            else Console.WriteLine("No Ip EndPoint Found!!!");
        }
    }
}

using System.Net;
using System.Net.Sockets;

namespace SocketH4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IPEndPoint? ipEndPoint = GetIPEndPoint();
            if (ipEndPoint != null) { new SocketServer(ipEndPoint);}
            Console.WriteLine("No Ip EndPoint Found!!!");
        }

        static IPEndPoint? GetIPEndPoint()
        {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork);
            IPAddress[] addressList = iPHostEntry.AddressList;
            //TODO pick IP Address more inteligently
            if (addressList == null || addressList[0] == null) return null;
            return new IPEndPoint(addressList[0], 8090);
        }
    }
}

using System.Net.Sockets;
using System.Net;

namespace IpClassLibrary
{
    public class Class1
    {
        public static IPEndPoint? GetIPEndPoint()
        {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork);
            IPAddress[] addressList = iPHostEntry.AddressList;
            //TODO pick IP Address more inteligently
            if (addressList == null || addressList[0] == null) return null;
            return new IPEndPoint(addressList[0], 8090);
        }
    }
}

using System.Net;
using System.Net.Sockets;

namespace ECommerce.Shared.Constants
{
    public static class Constants
    {
        public const string MyIpv4 = "192.168.1.11";

        public const string Email = "erenyeageraottitan1@gmail.com";

        public const string Password = "hzga iobj kxwv znqs";

        public static string GetMyIpv4()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipv4 = host.AddressList.FirstOrDefault(ip =>
                ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip));

            return ipv4?.ToString()!;
        }
    }
}
using System.Net;
using System.Net.Sockets;

namespace ECommerce.Shared.Constants
{
    public static class Constants
    {
        public static int CategoryCount { get; } = 10000;
        public static int ProductCount { get; } = 100000;
        public static int CountryCount { get; } = 100;
        public static int StateCount { get; } = 1000;
        public static int CityCount { get; } = 10000;
        public static int RolePermissionCount { get; } = 1000;
        public static int UserCount { get; } = 10000;
        public static int AddressCount { get; } = 20000;
        public static int CartItemCount { get; } = 10000;
        public static int OrderItemCount { get; } = 10000;
        public static int OrderCount { get; } = 1000;
        public static string MyIpv4 { get; } = "192.168.1.11";
        public static string Email { get; } = "erenyeageraottitan1@gmail.com";
        public static string Password { get; } = "hzga iobj kxwv znqs";

        public static string GetMyIpv4()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipv4 = host.AddressList.FirstOrDefault(ip =>
                ip.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip));

            return ipv4?.ToString()!;
        }
    }
}
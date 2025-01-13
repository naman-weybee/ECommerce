using ECommerce.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Application.Services
{
    public class MD5Service : IMD5Service
    {
        public MD5Service()
        {
        }

        public string ComputeMD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("Input for MD5 is required.");

            var inputBytes = Encoding.UTF8.GetBytes(input);

            var hashBytes = MD5.HashData(inputBytes);

            var sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
using System;
using System.Text;

namespace Snapshooter
{
    public static class SHA256Hash
    {
        public static string Hash(string value)
        {
            using var SHA256 = System.Security.Cryptography.SHA256.Create();

            return Convert.ToBase64String(SHA256.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}

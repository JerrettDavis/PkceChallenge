using System.Security.Cryptography;
using System.Text;

namespace Pkce.Challenge.Common;

public static class ExtensionMethods
{
    public static byte[] ToSHA256ByteArray(this string input)
    {
        #if NET5_0_OR_GREATER
            return SHA256.HashData( Encoding.UTF8.GetBytes(input));
        #else
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        #endif
    }
}
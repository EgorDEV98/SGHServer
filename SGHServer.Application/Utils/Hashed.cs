using System.Security.Cryptography;
using System.Text;

namespace SGHServer.Application.Utils;

public static class Hashed
{
    public static string Encrypt(string password)
    {
        var md5 = new MD5CryptoServiceProvider();
        var data = Encoding.ASCII.GetBytes(password);
        var md5data = md5.ComputeHash(data);
        var hashedPassword = Encoding.Default.GetString(md5data);

        return hashedPassword;
    }
}
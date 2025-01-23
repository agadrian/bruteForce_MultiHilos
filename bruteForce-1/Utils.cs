using System.Security.Cryptography;
using System.Text;

namespace bruteForce_1;

public class Utils
{
    public static string HashPass(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256Hash.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
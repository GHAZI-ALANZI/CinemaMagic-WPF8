using System.Security.Cryptography;
using System.Text;

namespace CinemaMagic.ViewModels
{
    public class PTChung
    {
        // Check if a string contains Unicode characters
        public static bool ContainsUnicodeCharacter(string input)
        {
            foreach (char c in input)
            {
                if (c > 127)
                {
                    return true;
                }
            }

            return false;
        }



        // MD5 encryption
        public static string EncryptMD5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

    }
}

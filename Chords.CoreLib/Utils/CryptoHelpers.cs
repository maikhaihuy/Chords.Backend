using System;
using System.Security.Cryptography;
using System.Text;

namespace Chords.CoreLib.Utils
{
    public static class CryptoHelpers
    {
        public static string PasswordHash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(PasswordHash(password), hash) == 0;
        }
    }
}
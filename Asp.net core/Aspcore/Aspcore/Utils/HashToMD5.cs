using System;
using System.Security.Cryptography;
using System.Text;

namespace Aspcore.Utils
{
    public static class HashToMD5
    {
        public static string Hash(this string input)
        {

            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));


            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }


            return sBuilder.ToString();
        }

        public static Boolean checkMd5Hash(this string input, string hash)
        {
            MD5 md5Hash = MD5.Create();

            string hashOfInput = input.Hash();


            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
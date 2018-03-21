using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AITAwards
{
    public class MD5
    {
        public string EncodePassword(string password)
        {
            return GetMd5Hash(password);
        }

        private string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5provider.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
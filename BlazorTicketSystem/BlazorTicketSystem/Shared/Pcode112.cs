using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;


namespace BlazorTicketSystem.Shared
{
    public class Pcode112
    {
        private static string Key = "dofkrfaosrdeng.Asemo09234wsdfwef";
        private static string IV = "zxcvbJo4Tek78fgh";
        public static string Encrypt(string text)
        {
            try
            {
                byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
                aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
                crypto.Dispose();
                return Convert.ToBase64String(encrypted);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string Decrypt(string encrypted)
        {
            try
            {
                byte[] encryptedbytes = Convert.FromBase64String(encrypted);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
                aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
                crypto.Dispose();
                return System.Text.ASCIIEncoding.ASCII.GetString(secret);
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}

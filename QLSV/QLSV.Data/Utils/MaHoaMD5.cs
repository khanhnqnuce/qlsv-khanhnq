using System;
using QLSV.Core.Utils;

namespace QLSV.Data.Utils
{
    public static class MaHoaMd5
    {
        private static byte[] EncryptData(string data)
        {
            try
            {
                var md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
                var encoder = new System.Text.UTF8Encoding();
                var hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
                return hashedBytes;
            }
            catch (Exception ex)
            {
                Log2File.LogExceptionToFile(ex);
                return null;
            }
        }

        public static string Md5(string data)
        {
            return BitConverter.ToString(EncryptData(data)).Replace("-", "").ToLower();
        }
    }
}

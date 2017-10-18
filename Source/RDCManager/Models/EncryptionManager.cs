using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RDCManager.Models
{
    public class EncryptionManager : IEncryptionManager
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionManager()
        {
            _key = Encoding.UTF8.GetBytes("SKC1CNVUMRIWGLQ5BBDZNBPL35AWPJKR");
            _iv = Encoding.UTF8.GetBytes("CC6F553885CAC498");
        }

        public string AesEncrypt(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            AesManaged aes = new AesManaged();
            ICryptoTransform encryptor = aes.CreateEncryptor(_key, _iv);

            byte[] data = Encoding.UTF8.GetBytes(value);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string AesDecrypt(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            AesManaged aes = new AesManaged();
            ICryptoTransform decryptor = aes.CreateDecryptor(_key, _iv);

            byte[] data = Convert.FromBase64String(value);

            using (MemoryStream ms = new MemoryStream(data))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
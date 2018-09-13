using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DGN.Encryption.Cryptography
{
    public class Provider
    {
        private string _key;
        public Provider(string Key)
        {
            _key = Key;
        }

        private static byte[] Byte16(string args)
        {
            
            char[] arrayChar = args.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }

        /// <param name="text">Şifrelenecek text bu parametre ile gönderilir</param>
        public string Encrypt(string text)
        {            
            string sonuc = "";
            if (text == "" || text == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                RijndaelManaged dec = new RijndaelManaged();
                dec.Mode = CipherMode.CBC;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(Byte16(_key), Byte16(String.Join("",_key.Reverse()))), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cs);
                writer.Write(text);
                writer.Flush();
                cs.FlushFinalBlock();
                writer.Flush();
                sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                writer.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return sonuc;
        }

        /// <param name="encryptedText">Şifrelenmiş text bu parametre ile gönderilir</param>
        public string TryDecrypt(string encryptedText)
        {
            string strSonuc = "";
            if (string.IsNullOrEmpty(encryptedText))
                throw new Exception("Çözümlenecek metin bulunamadı!");
            try
            {
                RijndaelManaged cp = new RijndaelManaged();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText));
                CryptoStream cs = new CryptoStream(ms, cp.CreateDecryptor(Byte16(_key),Byte16(String.Join("", _key.Reverse()))), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strSonuc = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Şifrelenmiş veri okunurken hata oluştu!" + ex.Message);
            }
            return strSonuc;
        }
    }

   
}

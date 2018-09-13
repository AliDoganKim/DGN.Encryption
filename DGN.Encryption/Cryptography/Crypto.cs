using System;

namespace DGN.Encryption.Cryptography
{
    public class Crypto
    {
        private Provider cryptos;

        public Crypto(string Key)
        {
            cryptos = new Provider(Key);
        }

        /// <summary>
        /// String bir nesneyi şifrelemeyi dener. Başarılı olamazsa hata mesajı döndürür.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string TryEncrypt(string val)
        {
            var retValue = cryptos.Encrypt(val);
            try
            {
                return retValue;
            }
            catch (Exception ex)
            {
                return retValue;
            }
        }

        /// <summary>
        /// Gelen string nesneyi çözmeye dener. Başarılı olamazsa hata mesajı döndürür.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string TryDecrypt(string val)
        {
            var retValue = cryptos.TryDecrypt(val);
            try
            {
                return retValue;
            }
            catch (Exception)
            {
                return retValue;
            }
        }
    }
}

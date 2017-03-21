using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.SymetricalCrypt
{
    public sealed class AES : SymetricalCrypt
    {
        public static readonly int[] SIZE_KEY_AUTHORIZED = { 16, 24, 32 };
        public static readonly int SIZE_IV_AUTHORIZED = 16;

        public override byte[] Key { get { return _key; } }
        public byte[] IV { get { return _iv; } }

        private byte[] _key;
        private byte[] _iv;

        #region Constructor
        public AES()
        {
            CreateCouple();
        }

        public AES(byte[] key, byte[] iv)
        {
            AssignKey(key);
            AssignIV(iv);
        }

        public AES(string key, string iv)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(key);
            AssignKey(bKey);
            byte[] bIv = Encoding.UTF8.GetBytes(key);
            AssignIV(bIv);
        }
        #endregion

        #region Create keys
        public override void CreateKey()
        {
            byte[] key = Encoding.UTF8.GetBytes("UnifiedLibraryV1");
            AssignKey(key);
        }

        public void CreateIV()
        {
            byte[] iv = Encoding.UTF8.GetBytes("UnifiedLibraryV1");
            AssignIV(iv);
        }

        public void CreateCouple()
        {
            CreateIV();
            CreateKey();
        }
        #endregion

        #region Crypt/Decrypt
        public override byte[] Decrypt(byte[] dataCrypted)
        {
            byte[] dataClear = new byte[] { };
            try
            {
                RijndaelManaged rijndael = new RijndaelManaged();
                rijndael.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = rijndael.CreateDecryptor(this.Key, this.IV);
                MemoryStream ms = new MemoryStream(dataCrypted);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                dataClear = new byte[dataCrypted.Length];

                int decryptedByteCount = cs.Read(dataClear, 0, dataClear.Length);

                ms.Close();
                cs.Close();
            }
            catch
            {

            }
            return dataClear;
        }

        public override byte[] Encrypt(byte[] dataToCrypt)
        {
            MemoryStream ms;
            CryptoStream cs;
            byte[] dataCrypted = new byte[] { };
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.Mode = CipherMode.CBC;
                ICryptoTransform aesEncryptor = aes.CreateEncryptor(this.Key, this.IV);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
                cs.Write(dataToCrypt, 0, dataToCrypt.Length);
                cs.FlushFinalBlock();
                dataCrypted = ms.ToArray();
                ms.Close();
                cs.Close();
            }
            catch
            {

            }
            return dataCrypted;
        }
        #endregion

        #region Inspector of keys
        public override void AssignKey(byte[] key)
        {
            if (CheckKey(key))
                this._key = key;
            else
                throw new UnifiedLibraryV1.Security.SymetricalCrypt.Exception.WrongSizeKeyException("Wrong size about AES' Key");
        }

        public override bool CheckKey(byte[] key)
        {
            return SIZE_KEY_AUTHORIZED.Contains(key.Length);
        }

        public void AssignIV(byte[] iv)
        {
            if (CheckIV(iv))
                this._iv = iv;
            else
                throw new UnifiedLibraryV1.Security.SymetricalCrypt.Exception.WrongSizeKeyException("Wrong size about AES' IV");
        }

        public bool CheckIV(byte[] iv)
        {
            return iv.Length == SIZE_IV_AUTHORIZED;
        }
        #endregion

        #region Static usage
        //To do
        #endregion
    }
}

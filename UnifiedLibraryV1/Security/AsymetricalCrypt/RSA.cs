using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.AsymetricalCrypt
{
    public sealed class RSA : AsymetricalCrypt
    {
        public override byte[] PrivateKey { get { return _public; } }
        public override byte[] PublicKey { get { return _private; } }

        private byte[] _public;
        private byte[] _private;
        private UsageMode _usage;

        public RSA()
        {
            _usage = UsageMode.Creating;
            CreateCouple();
        }

        public RSA(UsageMode mode, byte[] key)
        {
            this._usage = mode;
            if (_usage == UsageMode.Encrypting)
                AssignPublicKey(key);
            else if (_usage == UsageMode.Decrypting)
                AssignPrivateKey(key);
            else
                CreateCouple();
        }

        public RSA(UsageMode mode, RSAParameters key)
        {
            this._usage = mode;
            if (_usage == UsageMode.Encrypting)
                AssignPublicKey(this.RSAKeyToArrayByte(key));
            else if (_usage == UsageMode.Decrypting)
                AssignPrivateKey(this.RSAKeyToArrayByte(key));
            else
                CreateCouple();
        }

        public override void CreateCouple()
        {
            if (_usage == UsageMode.Creating)
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSAParameters publicKey = RSA.ExportParameters(false);
                    RSAParameters privateKey = RSA.ExportParameters(true);

                    this._private = RSAKeyToArrayByte(privateKey);
                    this._public = RSAKeyToArrayByte(publicKey);
                }
            }
            else
            {
                //Exception
            }
        }

        public override void AssignPrivateKey(byte[] key)
        {
            if (this._usage == UsageMode.Decrypting)
            {
                this._private = key;
            }
            else
            {
                //Exception
            }
        }

        public override void AssignPublicKey(byte[] key)
        {
            if (this._usage == UsageMode.Encrypting)
            {
                this._public = key;
            }
            else
            {
                //Exception
            }
        }

        public override byte[] Decrypt(byte[] dataCrypted)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(ArrayByteToRSAKey(this.PrivateKey));
                    encryptedData = RSA.Encrypt(dataCrypted, false);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public override byte[] Encrypt(byte[] dataClear)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSAParameters param = ArrayByteToRSAKey(this.PublicKey);
                    RSA.ImportParameters(param);
                    encryptedData = RSA.Encrypt(dataClear, false);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private byte[] RSAKeyToArrayByte(RSAParameters key)
        {
            IO.Stream.DataWriter writer = new IO.Stream.DataWriter(IO.Stream.WritingMode.BigEndian);
            writer.Write(key);
            byte[] buff = writer.Data;
            writer.Close();
            return buff;
        }


        private RSAParameters ArrayByteToRSAKey(byte[] data)
        {
            IO.Stream.DataReader reader = new IO.Stream.DataReader(IO.Stream.ReadingMode.BigEndian, data);
            RSAParameters key = reader.ReadObject<RSAParameters>();
            reader.Close();
            return key;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Security
{
    /// <summary>
    /// 解决 RSA加密算法的长度限制问题
    /// </summary>
    public class RSACrypto
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static private RSAParameters DeserializeRSAParameters(string key)
        {
            key = Encoding.Unicode.GetString(Convert.FromBase64String(key));
            //get a stream from the string
            var sr = new System.IO.StringReader(key);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            return (RSAParameters)xs.Deserialize(sr);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="RSAKeyInfo">密钥</param>
        /// <returns></returns>
        static public string RSAEncrypt(string dataToEncrypt, string RSAKeyInfo)
        {

            RSAParameters RSAParameters = DeserializeRSAParameters(RSAKeyInfo);

            byte[] encryptedData = RSAEncrypt(System.Text.Encoding.Unicode.GetBytes(dataToEncrypt), RSAParameters, false);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="dataToDecrypt"></param>
        /// <param name="RSAKeyInfo">密钥</param>
        /// <returns></returns>
        static public string RSADecrypt(string dataToDecrypt, string RSAKeyInfo)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();

            RSAParameters RSAParameters = DeserializeRSAParameters(RSAKeyInfo);

            byte[] encryptedData = RSADecrypt(Convert.FromBase64String(dataToDecrypt), RSAParameters, false);

            return System.Text.Encoding.Unicode.GetString(encryptedData);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="DataToEncrypt"></param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {


                //byte[] encryptedData;
                ////Create a new instance of RSACryptoServiceProvider.
                //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                //{

                //    //Import the RSA Key information. This only needs
                //    //toinclude the public key information.
                //    RSA.ImportParameters(RSAKeyInfo);

                //    //Encrypt the passed byte array and specify OAEP padding.  
                //    //OAEP padding is only available on Microsoft Windows XP or
                //    //later.  
                //    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                //}


                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);

                    int keySize = RSA.KeySize / 8;
                    int bufferSize = keySize - 11;
                    byte[] buffer = new byte[bufferSize];
                    MemoryStream msInput = new MemoryStream(DataToEncrypt);
                    MemoryStream msOutput = new MemoryStream();
                    int readLen = msInput.Read(buffer, 0, bufferSize);
                    while (readLen > 0)
                    {
                        byte[] dataToEnc = new byte[readLen];
                        Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                        byte[] encData = RSA.Encrypt(dataToEnc, false);
                        msOutput.Write(encData, 0, encData.Length);
                        readLen = msInput.Read(buffer, 0, bufferSize);
                    }
                    msInput.Close();
                    encryptedData = msOutput.ToArray();    //得到加密结果
                    msOutput.Close();
                    RSA.Clear();
                }
                return encryptedData;


            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="DataToDecrypt"></param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    ////Import the RSA Key information. This needs
                    ////to include the private key information.
                    //RSA.ImportParameters(RSAKeyInfo);

                    ////Decrypt the passed byte array and specify OAEP padding.  
                    ////OAEP padding is only available on Microsoft Windows XP or
                    ////later.  
                    //decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);

                    RSA.ImportParameters(RSAKeyInfo);
                    int keySize = RSA.KeySize / 8;
                    byte[] buffer = new byte[keySize];
                    MemoryStream msInput = new MemoryStream(DataToDecrypt);
                    MemoryStream msOutput = new MemoryStream();
                    int readLen = msInput.Read(buffer, 0, keySize);
                    while (readLen > 0)
                    {
                        byte[] dataToDec = new byte[readLen];
                        Array.Copy(buffer, 0, dataToDec, 0, readLen);
                        byte[] decData = RSA.Decrypt(dataToDec, false);
                        msOutput.Write(decData, 0, decData.Length);
                        readLen = msInput.Read(buffer, 0, keySize);
                    }
                    msInput.Close();
                    decryptedData = msOutput.ToArray();    //得到解密结果
                    msOutput.Close();
                    RSA.Clear();
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
    }
}

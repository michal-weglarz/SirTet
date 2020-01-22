using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SirTetLogic
{
    /// <summary>
    /// The main SaveScore class
    /// Contains all method for performing SaveScore function
    /// </summary>
    public class SaveScore
    {
        /// <summary>
        /// Method responsible for encrypting
        /// </summary>
        /// <param name="input">Variable containing content to encrypt</param>
        /// <param name="key">Variable containing encrypt key</param>
        /// <returns></returns>
        public static string EncryptString(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// Method responsible for decrypting
        /// </summary>
        /// <param name="input">Variable containing content to decrypt</param>
        /// <param name="key">Variable containing decrypt key</param>
        /// <returns></returns>
        public static string DecryptString(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// Method responsible for saving to file
        /// </summary>
        /// <param name="value">Table containing stuff to save</param>
        /// <param name="dirPath">Variable containing save path</param>
        /// <param name="fileName">Variable containing file name</param>
        public static void SaveFile(string[] value, string dirPath, string fileName)
        {
            if(Directory.Exists(dirPath))
            {
                File.WriteAllLines(dirPath + "\\" + fileName, value);
                return;
            }
            Directory.CreateDirectory(dirPath);
            SaveFile(value, dirPath, fileName);
            //File.WriteAllLines(@"C:\Users\Public\SirTet\Records.txt", lines);
        }
        /// <summary>
        /// Method for reading file
        /// </summary>
        /// <param name="path">Variable contains path to file</param>
        /// <returns>Return table containing read content of file </returns>
        public static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
        }      
            
    }
}

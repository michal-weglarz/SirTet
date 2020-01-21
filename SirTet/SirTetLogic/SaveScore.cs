using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SirTetLogic
{
    public class SaveScore
    {
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

        public static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
        }      
            
    }
}

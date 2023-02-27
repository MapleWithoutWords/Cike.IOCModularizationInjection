using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class StreamExtensions
    {
        /// <summary>
        /// 计算流的MD5
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToMd5(this Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                stream.Position = 0;
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("x").Length == 1 ? "0" + computeBytes[i].ToString("x") : computeBytes[i].ToString("x");
                }
                stream.Position = 0;
                return result;
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="st"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static string SaveFile(this Stream st, string savePath)
        {
            using (var fs = File.Open(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                st.CopyTo(fs);
                return savePath;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string FistCharToUpper(this string str)
        {
            if (str.Length < 1)
            {
                return string.Empty;
            }
            if (str[0] >= 'A' && str[0] <= 'Z')
            {
                return str;
            }
            return (char)(str[0] - 32) + str.Substring(1);
        }
        public static string FistCharToLower(this string str)
        {
            if (str.Length < 1)
            {
                return string.Empty;
            }
            if (str[0] >= 'a' && str[0] <= 'z')
            {
                return str;
            }
            return (char)(str[0] + 32) + str.Substring(1);
        }
        /// <summary>
        /// string 'str' whether null and empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNoNullAndNoEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        /// <summary>
        /// string 'str' whether is null or empty or Guid empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrGuidEmpty(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return true;
            }
            if (str == Guid.Empty.ToString())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// try convert string type to int type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseInt(this string str, out int val)
        {
            return int.TryParse(str, out val);
        }
        public static int ParseInt(this string str)
        {
            return int.Parse(str);
        }
        /// <summary>
        /// try convert string type to long type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseLong(this string str, out long val)
        {
            return long.TryParse(str, out val);
        }
        public static long ParseLong(this string str)
        {
            return long.Parse(str);
        }
        /// <summary>
        /// try convert string type to float type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseFloat(this string str, out float val)
        {
            return float.TryParse(str, out val);
        }
        public static float ParseFloat(this string str)
        {
            return float.Parse(str);
        }
        /// <summary>
        /// try convert string type to Double type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseDouble(this string str, out double val)
        {
            return double.TryParse(str, out val);
        }
        public static double ParseDouble(this string str)
        {
            return double.Parse(str);
        }
        /// <summary>
        ///  try convert string type to DateTime type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseDateTime(this string str, out DateTime val)
        {
            return DateTime.TryParse(str, out val);
        }
        public static DateTime ParseDateTime(this string str)
        {
            return DateTime.Parse(str);
        }
        /// <summary>
        /// try convert string type to Guid type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseGuid(this string str, out Guid val)
        {
            return Guid.TryParse(str, out val);
        }
        /// <summary>
        /// convert string type to Guid type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Guid ParseGuid(this string str)
        {
            if (Guid.TryParse(str, out Guid val) == false)
            {
                throw new ArgumentException($"the string '{str}' not is Guid");
            }
            return val;
        }

        /// <summary>
        ///  try convert string type to decimal type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseDecimal(this string str, out decimal val)
        {
            return decimal.TryParse(str, out val);
        }
        public static decimal ParseDecimal(this string str)
        {
            return decimal.Parse(str);
        }

        /// <summary>
        ///  xml deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(this string xmlContent) where T : class, new()
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                return xs.Deserialize(ms) as T;
            }
        }

        /// <summary>
        /// if path not exists the create a directory.or else return directory path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string CreateDirectory(this string filePath)
        {
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }

            return filePath;
        }

        /// <summary>
        ///  create date format directory for 'filePath'
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string CreateDateDirectory(this string filePath)
        {
            filePath = filePath.CreateDirectory();

            filePath = Path.Combine(filePath, DateTime.Now.ToString("yyyy/MM/dd"));
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
            return filePath;
        }

        /// <summary>
        /// 'paht' whether root path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsPathRooted(this string path)
        {
            return Path.IsPathRooted(path);
        }
    }
}

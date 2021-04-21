using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Winner.Extends
{
    public class FileHelper
    {
        private const string FileSeparator = "|";
        private const string FileIdAndNameSeparator = "&&";

        /// <summary>
        /// 获取文件的编码格式
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Encoding GetFileEncodeType(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    byte[] buffer = br.ReadBytes(2);
                    if (buffer[0] >= 0xEF)
                    {
                        if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                        {
                            return Encoding.UTF8;
                        }
                        else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                        {
                            return Encoding.BigEndianUnicode;
                        }
                        else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                        {
                            return Encoding.Unicode;
                        }
                        else
                        {
                            return Encoding.UTF8;
                        }
                    }
                    else
                    {
                        return Encoding.UTF8;
                    }
                }
            }
            catch (Exception)
            {
                return Encoding.UTF8;
            }
        }

        /// <summary>
        /// 获取文件的md5值
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetFileMd5Hash(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
            {
                throw new ArgumentNullException(nameof(fullPath));
            }

            using (var fs = new FileStream(fullPath, FileMode.Open))
            {
                return GetStreamMd5Hash(fs);
            }
        }

        /// <summary>
        /// 获取文件流的hash值
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string GetStreamMd5Hash(Stream fs)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var bytHash = md5.ComputeHash(fs);
                md5.Clear();
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                return sTemp.ToLower();
            }
        }

        /// <summary>
        /// 获取MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5(string input)
        {
            var bytValue = Encoding.UTF8.GetBytes(input);
            using (var stream = new MemoryStream(bytValue))
            {
                return GetStreamMd5Hash(stream);
            }
        }

        /// <summary>
        /// 将文件列表转为分隔符表示的字符串
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        //public static string MergeFileToString(FileInfo[] files)
        //{
        //    var result = string.Join(FileSeparator,
        //        files.Select(n =>
        //            $"{n.FileId}{FileIdAndNameSeparator}{n.FileName?.Replace(FileIdAndNameSeparator, string.Empty)}"));

        //    return result;
        //}

        //public static List<FileInfo> ParseStringToFile(string filesString)
        //{
        //    if (string.IsNullOrEmpty(filesString))
        //    {
        //        return new List<FileInfo>();
        //    }

        //    var result = new List<FileInfo>();
        //    var fileItems = filesString.Split(FileSeparator);

        //    foreach (var fileItem in fileItems)
        //    {
        //        if (string.IsNullOrEmpty(fileItem))
        //        {
        //            continue;
        //        }

        //        var file = new FileInfo();

        //        var data = fileItem.Split(FileIdAndNameSeparator);
        //        if (data.Length == 1)
        //        {
        //            file.FileId = data[0];
        //            result.Add(file);
        //            continue;
        //        }

        //        if (data.Length == 2)
        //        {
        //            file.FileId = data[0];
        //            file.FileName = data[1];
        //            result.Add(file);
        //            continue;
        //        }
        //    }

        //    return result;
        //}

        public static string GetFileUrl(string fileStr, string fsDomain)
        {
            var result = new List<string>();
            var list = string.IsNullOrWhiteSpace(fileStr) ? null : fileStr.Trim().Split(";")?.ToList();

            list?.ForEach(_ => result.Add($"{fsDomain.Trim('/')}/{_}"));

            return result?.FirstOrDefault();
        }
    }
}

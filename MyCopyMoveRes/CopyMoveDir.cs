using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopyMoveRes
{
    public class CopyMoveDir
    {
        private const string defaultString = "target = \"\"";
        private const string configPath = @"Config.txt";

        private string path;

        public CopyMoveDir()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    CreateDefaultFile();
                    Console.WriteLine("Create Default File");
                }

                using (var sr = new StreamReader(configPath))
                {
                    var s1 = sr.ReadLine();
                    var start = s1.IndexOf('"') + 1;
                    var end = s1.LastIndexOf('"');
                    path = s1.Substring(start, end - start);
                    if (string.IsNullOrEmpty(path))
                    {
                        Console.WriteLine("Path is null or Empty");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CopyAndMove()
        {
            //清空原来的文件夹 但是保留当前自己
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Path is null or Empty");
                return;
            }

            DirectoryInfo from = new DirectoryInfo(path);

            DirectoryInfo to = from.Parent;

            string fromName = from.Name;
            string toName = from.Parent.FullName + @"\";

            //如果被复制的文件夹是空的
            if (from.GetFiles().Length == 0 || from.GetDirectories().Length == 0)
            {
                Console.WriteLine("复制文件夹是空的");
                return;
            }

            //先删除
            foreach (var item in to.GetDirectories())
            {
                if (item.Name != fromName)
                {
                    Directory.Delete(item.FullName, true);
                }
            }
            foreach (var item in to.GetFiles())
            {
                File.Delete(item.FullName);
            }

            Console.WriteLine("删除OK");

            //后移动
            foreach (var item in from.GetDirectories())
            {
                Directory.Move(item.FullName, toName + item.Name);
            }
            foreach (var item in from.GetFiles())
            {
                File.Move(item.FullName, toName+item.Name);
            }

            Console.WriteLine("移动OK");
        }

        public void CreateDefaultFile()
        {
            using (var fs = File.Open(configPath, FileMode.OpenOrCreate))
            {
                var bs = Encoding.UTF8.GetBytes(defaultString);
                fs.Write(bs, 0, bs.Length);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mascot
{
    class ModelInfo
    {
        private string modelPath;
        private string [] motionNames;
        public ModelInfo(string modelPath)
        {
            this.modelPath = modelPath;
            motionNames = getMotionNames(modelPath);
        }

        static string [] getMotionPathes(string modelPath)
        {
            return FindFilePathes(Path.GetDirectoryName(modelPath), ".*.vmd", "*.vmd", false);
        }

        static string[] getMotionNames(string modelPath)
        {
            return FindFileNames(Path.GetDirectoryName(modelPath), ".*.vmd", "*.vmd", false); ;
        }

        static string [] FindFilePathes(string path, string pattern, string wildcard, bool ignorecase)
        {
            List<string> lst = new List<string>();
            RegexOptions opt = RegexOptions.None;
            if (ignorecase) opt |= RegexOptions.IgnoreCase;
            Regex reg = new Regex(pattern, opt);
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(wildcard);
            foreach (FileInfo f in files)
            {
                if (reg.IsMatch(f.FullName))
                {
                    lst.Add(f.FullName);
                }
            }
            return lst.ToArray();
        }

        static string[] FindFileNames(string path, string pattern, string wildcard, bool ignorecase)
        {
            string[] filePathes;
            filePathes = FindFilePathes(path, pattern, wildcard, ignorecase);
            List<string> lst = new List<string>();
            foreach (string f in filePathes)
            {
                lst.Add(Path.GetFileName(f));
            }
            return lst.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YoukuAutomation
{
    public class FileAction
    {
        /// <summary>
        /// 单例
        /// </summary>
        /// <value>The instance.</value>
        public static FileAction Instance { get { return Singleton<FileAction>.GetInstance(); } }

        public void FileMove(string oldpath, string newpath)
        {
            File.Move(oldpath, newpath);
            Log.InfoLog("文件转移成功,从" + oldpath + "转移到" + newpath);
        }
    }
}

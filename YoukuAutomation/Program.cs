using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace YoukuAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                //YoukuTimer yt = new YoukuTimer();
                YoukuUtility.Instance.SetupFF();
                while (true)
                {
                    DirectoryInfo TheFolder = new DirectoryInfo(AppSetting.GetValue("FolderPath"));
                    foreach (FileInfo upFile in TheFolder.GetFiles())
                    {
                        YoukuUtility.Instance.DoUpload(upFile.FullName);
                        Log.InfoLog("文件上传成功,文件路径：" + upFile.FullName);
                        FileAction.Instance.FileMove(upFile.FullName, AppSetting.GetValue("VideoPath") + "\\" + upFile.Name);
                        Thread.Sleep(5000);

                    }
                    Thread.Sleep(10000);
                }
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}

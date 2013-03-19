using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace YoukuAutomation
{
    public class YoukuTimer
    {
        public YoukuTimer()
        {
            YoukuUtility.Instance.SetupFF();     
            System.Timers.Timer t = new System.Timers.Timer(30000); 
            t.Elapsed += new System.Timers.ElapsedEventHandler(YoukuAutomation);  
            t.AutoReset = true; 
            t.Enabled = true;                  
        }

        public void YoukuAutomation(object source, System.Timers.ElapsedEventArgs e)
        {

            DirectoryInfo TheFolder = new DirectoryInfo(AppSetting.GetValue("FolderPath"));
            foreach (FileInfo upFile in TheFolder.GetFiles())
            {
                YoukuUtility.Instance.DoUpload(upFile.FullName);
                Log.InfoLog("文件上传成功,文件路径：" + upFile.FullName);
                FileAction.Instance.FileMove(upFile.FullName, AppSetting.GetValue("VideoPath") + "\\" + upFile.Name);
                Thread.Sleep(5000);

            }            
        }


    }
}

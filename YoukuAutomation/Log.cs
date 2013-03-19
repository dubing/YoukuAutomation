using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace YoukuAutomation
{
    public class Log
    {
        public static void WarnLog(string msg)
        {
            string dateTimeStr = DateTime.Now.ToString();

            log4net.LogManager.GetLogger("WarnLog").Warn(dateTimeStr + "$$" + msg);
        }

        public static void WarnLog(string msg, Exception ex)
        {
            string dateTimeStr = DateTime.Now.ToString();

            log4net.LogManager.GetLogger("WarnLog").Warn(dateTimeStr + "$$" + msg, ex);
        }

        public static void DebugLog(string msg)
        {
            string dateTimeStr = DateTime.Now.ToString();

            log4net.LogManager.GetLogger("DebugLog").Debug(dateTimeStr + "$$" + msg);
        }

        public static void DebugLog(string msg, Exception ex)
        {
            string dateTimeStr = DateTime.Now.ToString();

            log4net.LogManager.GetLogger("DebugLog").Debug(dateTimeStr + "$$" + msg, ex);
        }

        public static void InfoLog(string msg)
        {
            string dateTimeStr = DateTime.Now.ToString();
            msg = dateTimeStr + "$$" + msg;
            log4net.LogManager.GetLogger("InfoLog").Info(msg+"\r\n");
            Console.WriteLine(msg);
        }
    }
}

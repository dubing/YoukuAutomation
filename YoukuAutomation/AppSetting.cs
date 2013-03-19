using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace YoukuAutomation
{
    public class AppSetting
    {
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BSG.Seyhoon.WebUI.Classes
{
    public class AppSettings
    {
        public static string GetKeyData(string key)
        {

            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }
    }
}
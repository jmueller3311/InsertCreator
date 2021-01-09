﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    /// <summary>
    /// Read and write App.config
    /// </summary>
    class AppSettingReaderWriter
    {
        /// <summary>
        /// Read setting in entered section
        /// </summary>
        /// <param name="configSection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadSetting(KeyName key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key.ToString()] ?? "Not Found";
                
            }
            catch (ConfigurationErrorsException)
            {
                return "";
            }
        }

        /// <summary>
        /// Write Setting in AppSettings
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteAppSetting(KeyName key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove(key.ToString());
                config.AppSettings.Settings.Add(key.ToString(), value);               
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }

            catch (ConfigurationErrorsException)
            {
               
            }
        }        
    }




    /// <summary>
    /// SettingsKeyEnum
    /// </summary>
    public enum KeyName
    {
        UseGreenscreen,
        ShowComponistAndAutor,
        UseLogo
    }

}

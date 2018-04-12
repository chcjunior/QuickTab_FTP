using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTab_FTP
{
    public class ConfigReader
    {

        public string ftp_UploadDirectory { get; set; }
        public string ftp_file { get; set; }  //this will need to be populated dynamically based on content of file system or ftp folder
        public string ftp_OutboundFolder { get; set; }
        public string ftp_host { get; set; }
        public string ftp_username { get; set; }
        public string ftp_password { get; set; }
        public string ftp_port { get; set; }
        public string ftp_InboundFolder { get; set; }
        public string ftp_DownloadDirectory { get; set; }
        public string ftp_UNCOutbound_Directory { get; set; }
        public string ftp_UNCInbound_Directory { get; set; }
        public string DB_Host_Name { get; set; }
        public string SQLProvider_Name { get; set; }
        public string Database_Name { get; set; }
        public string Database_Username { get; set; }
        public string Database_Password { get; set; }
        public string LogFilePath { get; set; }
        public string LogFileNameBase { get; set; }

        // Default Constructor
        // private ConfigReader()
        //  {
        //     ReadAllConfigProperties();
        // }

        /// Gets Instance...code concept copied from Sashi but I'm questioning why necessary....
        private static Lazy<ConfigReader> instance = new Lazy<ConfigReader>(() => new ConfigReader());

        public static ConfigReader Instance { get { return instance.Value; } }

        public void ReadAllConfigProperties()
        {
            if (ConfigurationManager.AppSettings["ftp_UploadDirectory"].Trim() != "")
            {
                ftp_UploadDirectory = (ConfigurationManager.AppSettings["ftp_UploadDirectory"]).Trim();
             }

            if (ConfigurationManager.AppSettings["ftp_file"].Trim() != "")
            {
                ftp_file = (ConfigurationManager.AppSettings["ftp_file"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_OutboundFolder"].Trim() != "")
            {
                ftp_OutboundFolder = (ConfigurationManager.AppSettings["ftp_OutboundFolder"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_host"].Trim() != "")
            {
                ftp_host = (ConfigurationManager.AppSettings["ftp_host"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_username"].Trim() != "")
            {
                ftp_username = (ConfigurationManager.AppSettings["ftp_username"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_password"].Trim() != "")
            {
                ftp_password = (ConfigurationManager.AppSettings["ftp_password"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_port"].Trim() != "")
            {
                ftp_port = (ConfigurationManager.AppSettings["ftp_port"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_InboundFolder"].Trim() != "")
            {
                ftp_InboundFolder = (ConfigurationManager.AppSettings["ftp_InboundFolder"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_DownloadDirectory"].Trim() != "")
            {
                ftp_DownloadDirectory = (ConfigurationManager.AppSettings["ftp_DownloadDirectory"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_UNCOutbound_Directory"].Trim() != "")
            {
                ftp_UNCOutbound_Directory = (ConfigurationManager.AppSettings["ftp_UNCOutbound_Directory"]).Trim();
            }

            if (ConfigurationManager.AppSettings["ftp_UNCInbound_Directory"].Trim() != "")
            {
                ftp_UNCInbound_Directory = (ConfigurationManager.AppSettings["ftp_UNCInbound_Directory"]).Trim();
            }
            if (ConfigurationManager.AppSettings["DB_Host_Name"].Trim() != "")
            {
                DB_Host_Name = (ConfigurationManager.AppSettings["DB_Host_Name"]).Trim();
            }

            if (ConfigurationManager.AppSettings["SQLProvider_Name"].Trim() != "")
            {
                SQLProvider_Name = (ConfigurationManager.AppSettings["SQLProvider_Name"]).Trim();
            }

            if (ConfigurationManager.AppSettings["Database_Name"].Trim() != "")
            {
                Database_Name = (ConfigurationManager.AppSettings["Database_Name"]).Trim();
            }

            if (ConfigurationManager.AppSettings["Database_Username"].Trim() != "")
            {
                Database_Username = (ConfigurationManager.AppSettings["Database_Username"]).Trim();
            }

            if (ConfigurationManager.AppSettings["Database_Password"].Trim() != "")
            {
                Database_Password = (ConfigurationManager.AppSettings["Database_Password"]).Trim();
            }
            if (ConfigurationManager.AppSettings["LogFilePath"].Trim() != "")
            {
                LogFilePath = (ConfigurationManager.AppSettings["LogFilePath"]).Trim();
            }
            if (ConfigurationManager.AppSettings["LogFileNameBase"].Trim() != "")
            {
                LogFileNameBase = (ConfigurationManager.AppSettings["LogFileNameBase"]).Trim();
            }
        }

        public void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        public string GetConnectionString_SQL(string DB_Host_Name, string SQLProvider_Name, string Database_Name, string DataBase_Username, string DataBase_Password)
        {
            return "Data Source=" + DB_Host_Name + "\\" + SQLProvider_Name + ";Initial Catalog=" + Database_Name + ";Persist Security Info=True;Connection Timeout=3600;User ID=" + DataBase_Username + ";Password=" + DataBase_Password;
        }


    }
}

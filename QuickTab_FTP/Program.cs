using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuickTab_FTP
{
    class Program
    {
        static void Main()
        {
            //@ is to circumvent escape characters
            //string source = @"C:\Users\chuck.collins\Documents\Visual Studio 2015\Projects\QuickTab_FTP\test_file\outbound\test_file.txt";
            //string destination = @"/outbound/";
            //string host = "filetransfer.alphaimpactrx.com";
            //string username = "quicktab";
            //string password = "N3wS0lut10n";
            //int port = 22;  //Port 22 is defaulted for SFTP upload
            //string remoteDirectory = @"/inbound/";
            //string localDirectory = @"C:\Users\chuck.collins\Documents\Visual Studio 2015\Projects\QuickTab_FTP\test_file\inbound\";
            int vdebug = 1;
            string ProcessFlag; 

            //instantiate ConfigReader from app.config
            ConfigReader objConfigReader = new ConfigReader();
            objConfigReader.ReadAllConfigProperties();

            //string ftp_sourcefile = ConfigReader.Instance.ftp_sourcefile;  //returns null as it references public string ftp_sourcefile { get; set; }
            string ftp_UploadDirectory = objConfigReader.ftp_UploadDirectory;
            string ftp_file = objConfigReader.ftp_file;
            string ftp_OutboundFolder = objConfigReader.ftp_OutboundFolder;
            string ftp_host = objConfigReader.ftp_host;
            string ftp_username = objConfigReader.ftp_username;
            string ftp_password = objConfigReader.ftp_password;
            string ftp_port = objConfigReader.ftp_port;
            string ftp_InboundFolder = objConfigReader.ftp_InboundFolder;
            string ftp_DownloadDirectory = objConfigReader.ftp_DownloadDirectory;
            string ftp_UNCOutbound_Directory = objConfigReader.ftp_UNCOutbound_Directory;
            string ftp_UNCInbound_Directory = objConfigReader.ftp_UNCInbound_Directory;
            string DB_Host_Name = objConfigReader.DB_Host_Name;
            string SQLProvider_Name = objConfigReader.SQLProvider_Name;
            string Database_Name = objConfigReader.Database_Name;
            string Database_Username = objConfigReader.Database_Username;
            string Database_Password = objConfigReader.Database_Password;

            string GetConnectionString_SQL = objConfigReader.GetConnectionString_SQL(DB_Host_Name, SQLProvider_Name, Database_Name, Database_Username, Database_Password);
 
            if (vdebug == 1)
             {
                Console.WriteLine(ftp_UploadDirectory);
                Console.WriteLine(ftp_file);
                Console.WriteLine(ftp_OutboundFolder);
                Console.WriteLine(ftp_host);
                Console.WriteLine(ftp_username);
                Console.WriteLine(ftp_password);
                Console.WriteLine(ftp_port);
                Console.WriteLine(ftp_InboundFolder);
                Console.WriteLine(ftp_DownloadDirectory);
                Console.WriteLine(ftp_UNCOutbound_Directory);
                Console.WriteLine(ftp_UNCInbound_Directory);
                Console.WriteLine(DB_Host_Name);
                Console.WriteLine(SQLProvider_Name);
                Console.WriteLine(Database_Name);
                Console.WriteLine(Database_Username);
                Console.WriteLine(Database_Password);
                Console.WriteLine(GetConnectionString_SQL);
            }

            //ConfigReader.Instance.

            //this works but not sure how to access "key, appSettings[key]"
            //objConfigReader.ReadAllSettings();
            File_Checker objInboundFileChecker = new File_Checker();

            //FTP_Class new_FTP_Class = new FTP_Class(); //instantiate new class 
            //new_FTP_Class.test_call();

            //check for file names in ftp outbound directory
            // objInboundFileChecker.CheckFolder(ftp_UNCInbound_Directory);

            ProcessFlag = "QT_Return";  
            objInboundFileChecker.CheckFolder(ProcessFlag, ftp_UNCInbound_Directory, GetConnectionString_SQL);

            //UploadSFTPFile(string host, string username, string password, string sourcefile, string destinationpath, int port)
            //FTP_Class.UploadSFTPFile(host, username, password, source, destination, port);


            //DownloadSFTPFile(string host, string username, string password, string remoteDirectory, string localDirectory, int port)
            //FTP_Class.DownloadSFTPFile(host, username, password, remoteDirectory, localDirectory, port);
        }



    }
}


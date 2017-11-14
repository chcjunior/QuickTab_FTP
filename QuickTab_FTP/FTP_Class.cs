using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;

namespace QuickTab_FTP
{
    class FTP_Class
    {

        public void test_call()
        {
            Console.WriteLine("FTP_Class called");
            //SftpClient client = new SftpClient(host, port, username, password);
        }

        public static void UploadSFTPFile(string host, string username, string password, string sourcefile, string destinationpath, int port)
        {
            using (SftpClient client = new SftpClient(host, port, username, password))  //'using' ensures the correct use of IDisposable objects 
            {
                client.Connect();
                client.ChangeDirectory(destinationpath);
                using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                {
                    client.BufferSize = 4 * 1024;
                    client.UploadFile(fs, Path.GetFileName(sourcefile));
                }
            }
        }

        public static void DownloadSFTPFile(string host, string username, string password, string remoteDirectory, string localDirectory, int port)
        {

            using (SftpClient client = new SftpClient(host, port, username, password))  //'using' ensures the correct use of IDisposable objects 
            {
                client.Connect();
                var files = client.ListDirectory(remoteDirectory);
                //I'll need to check to see if file is in the database config table
                foreach (var file in files)
                {
                    string remoteFileName = file.Name;

                    using (Stream file2download = File.OpenWrite(localDirectory + remoteFileName))
                        {
                          client.DownloadFile(remoteDirectory + remoteFileName, file2download);
                        }
                }

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;
namespace QuickTab_FTP
{
    /// <summary>
    /// For email related functionalties
    /// </summary>
    public class EmailHelper
    {

        /// <summary>
        /// Send Quick email for intermittent steps processed
        /// </summary>
        /// <param name="CREATE_PATH_COPY"></param>
        /// <param name="LogFileName"></param>
        /// <param name="strFrom"></param>
        /// <param name="strCC"></param>
        /// <param name="strTo"></param>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        /// <param name="smtpHost"></param>
        /// <param name="smtpPort"></param>
        /// <param name="usesSSL"></param>
        /// <param name="SSLUsername"></param>
        /// <param name="SSLPassword"></param>
        /// <param name="SSLDomain"></param>
        /// <returns></returns>
        public bool QUICK_EMAIL(string LogFileName, string strFrom, string strCC, string strTo, string strSubject, string strBody, string smtpHost, int smtpPort = 25,
        bool usesSSL = false,
        string SSLUsername = "",
        string SSLPassword = "",
        string SSLDomain = "")
        {
            StreamReader SR = null;
            //FileStream FileStr = null;
            string tempStr;
            try
            {

                if (LogFileName != "")
                {
                    using (FileStream FileStr = new FileStream(LogFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        if (!FileStr.Equals(String.Empty))
                        {
                            // Console.WriteLine("Error : Can Not Create " + unzip_log_file_name)
                            // 'GoTo EXIT_HERE
                            SR = new StreamReader(FileStr);
                            // creating a new StreamWriter and passing the filestream object fs as argument
                            SR.BaseStream.Seek(0, SeekOrigin.Begin);
                            // the seek method is used to move the cursor to next position to avoid text to be
                            // overwritten
                            tempStr = SR.ReadLine();
                            while (!(tempStr == null))
                            {
                                strBody = (strBody
                                            + (tempStr + "\r\n"));
                                tempStr = SR.ReadLine();
                            }

                        }

                        FileStr.Flush();
                        FileStr.Close();
                    }
                }

                SmtpClient smtpEmail = new SmtpClient(smtpHost, smtpPort);
                // create new SMTP client
                smtpEmail.EnableSsl = usesSSL;
                // true if SSL Authentication required
                if (usesSSL)
                {
                    // SSL authentication required?
                    if (((SSLUsername.Length == 0)
                                && (SSLPassword.Length == 0)))
                    {
                        // if both SSLUsername and SSLPassword are blank...
                        smtpEmail.UseDefaultCredentials = true;
                    }
                    else
                    {
                        // otherwise, we must create a new credential
                        if (SSLUsername.Length != 0)
                        {
                            // if SSLUsername is blank, use strFrom
                            smtpEmail.Credentials = new NetworkCredential(strFrom, SSLPassword, SSLDomain);
                        }
                        else
                        {
                            smtpEmail.Credentials = new NetworkCredential(SSLUsername, SSLPassword, SSLDomain);
                        }
                    }
                }
                smtpEmail.Send(strFrom, strTo, strSubject, strBody);
                // send email using text/plain content type and QuotedPrintable encoding
            }
            catch (Exception EX)
            {
                // if error, report it
                Dictionary<string, object> addProp = new Dictionary<string, object>();
                addProp.Add("Quick Email Failed Trace", EX.StackTrace);
                //  MsgBox(E.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Mail Send Error")
                EntLibLogHelper.LogMessage(EnumHelper.enCategoryType.ErrorData, "SMTP MAIL ERROR - Exception 11 : " + EX.Message + "\n\r", System.Diagnostics.TraceEventType.Information, addProp);
                //My.Computer.FileSystem.WriteAllText((CREATE_PATH_COPY + ("ERROR_DATA" + ".err")), ("SMTP MAIL ERROR - "
                //                + (EX.Message + "\r\n")), true);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Send Email with Completion Report VoiceProcessing files
        /// </summary>
        /// <param name="strFrom"></param>
        /// <param name="strTo"></param>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        /// <param name="smtpHost"></param>
        public void SendMail(string strFrom, string strTo, string strSubject, string strBody, string smtpHost)
        {
            int smtpPort = 25;
            bool usesSSL = false;
            string SSLUsername = null;
            string SSLPassword = null;
            string SSLDomain = null;
            try
            {
                System.Net.Mail.SmtpClient smtpEmail = new System.Net.Mail.SmtpClient(smtpHost, smtpPort);
                // create new SMTP client
                smtpEmail.EnableSsl = usesSSL;
                // true if SSL Authentication required
                if (usesSSL)
                {
                    // SSL authentication required?
                    if (((SSLUsername.Length == 0)
                                && (SSLPassword.Length == 0)))
                    {
                        // if both SSLUsername and SSLPassword are blank...
                        smtpEmail.UseDefaultCredentials = true;
                    }
                    else
                    {
                        // otherwise, we must create a new credential
                        if (SSLUsername == "")
                        {
                            // if SSLUsername is blank, use strFrom
                            smtpEmail.Credentials = new NetworkCredential(strFrom, SSLPassword, SSLDomain);
                        }
                        else
                        {
                            smtpEmail.Credentials = new NetworkCredential(SSLUsername, SSLPassword, SSLDomain);
                        }
                    }
                }
                smtpEmail.Send(strFrom, strTo, strSubject, strBody);
                // send email using text/plain content type and QuotedPrintable encoding
                Console.WriteLine((strSubject + " Email Sent Successfully"));
            }
            catch (Exception ex)
            {
                Dictionary<string, object> addProp = new Dictionary<string, object>();
                addProp.Add("Send Email Failed Trace", ex.StackTrace);
                Console.WriteLine(ex.Message, "Mail Send Error");
                EntLibLogHelper.LogMessage(EnumHelper.enCategoryType.IntlOutboundTranslationProcess, "Exception 12 : \r\n" + ex.Message + " " + "\r\n", TraceEventType.Error, addProp);
            }
        }


        /// <summary>
        /// Send Mail and terminate the process
        /// </summary>
        /// <param name="error_found"></param>
        /// <param name="Filecnt"></param>
        public void SendMailAndExit(bool error_found, int Filecnt)
        {
            if ((error_found == true))
            {
                SendMail(ConfigReader.Instance.VoiceTrancsriptionFromEmail, ConfigReader.Instance.VoiceTrancsriptionToEmail, "International Outbound Process is completed with errors", ("No files have been created and uploaded in "
                                + (ConfigReader.Instance.VendorStorageOutboundDir1 + " Vendor Upload Server, please check error logs.")), ConfigReader.Instance.SMTP_Server);

            }
            else
            {
                if (Filecnt == 0)
                {
                    SendMail(ConfigReader.Instance.VoiceTrancsriptionFromEmail, ConfigReader.Instance.VoiceTrancsriptionToEmail, "International Outbound Process is completed: " + ConfigReader.Instance.BATCH_ID_USER, "There were no files created for translation as there was no data to process ", ConfigReader.Instance.SMTP_Server);
                }
                else
                {
                    SendMail(ConfigReader.Instance.VoiceTrancsriptionFromEmail, ConfigReader.Instance.VoiceTrancsriptionToEmail, "International Outbound Process is completed: " + ConfigReader.Instance.BATCH_ID_USER, "Total : " + Filecnt.ToString() + " files have been created and uploaded in " + ConfigReader.Instance.VendorStorageOutboundDir1 + " Vendor Upload Server", ConfigReader.Instance.SMTP_Server);
                }
                //SendMail(ConfigReader.Instance.VoiceTrancsriptionFromEmail, ConfigReader.Instance.VoiceTrancsriptionToEmail, "International Outbound Process is completed", ("Total : "
                //               + (Filecnt.ToString() + (" files have been created and uploaded in "
                //               + (ConfigReader.Instance.VendorStorageOutboundDir1 + " Vendor Upload Server")))), ConfigReader.Instance.SMTP_Server);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace QuickTab_FTP
{
    public class File_Checker
    {

        public void CheckFolder(string ProcessFlag, string TargetDirectory, string String_SQL)
        {
            SqlConnection conn;
            SqlCommand command;
            

            try
            {
                DirectoryInfo di = new DirectoryInfo(TargetDirectory);
                FileInfo[] FileList = di.GetFiles();

                foreach (var fi in FileList)
                {
                    Console.WriteLine(fi.Name);
                    string fi_name = fi.Name; 

                    //check db table to confirm file 
                    conn = new SqlConnection();
                    conn.ConnectionString = String_SQL;
                    Console.WriteLine(conn.ConnectionString);
                    conn.Open();

                    //command = new SqlCommand("insert into dbo.chc_test([test column], [file_type]) select 'hey there', 'test file'", conn);
                    //cmd_results = command.ExecuteNonQuery();

                    command = new SqlCommand("select count(*) from dbo.chc_test where [file_type]='" + ProcessFlag + "' and [test column] = '" + fi_name + "'", conn);
                    command.CommandTimeout = 3600;
                    int file_count = (int) command.ExecuteScalar();
                    conn.Close();
                    Console.WriteLine(file_count); 

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with File_Checker");
            }

        }
    }


}
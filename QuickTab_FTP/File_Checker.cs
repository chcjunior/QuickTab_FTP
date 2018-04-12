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
        //need a method that loops through a directory and sub-directories as well
        public void CheckFolder(string ProcessFlag, string TargetDirectory, string String_SQL)
        {
            SqlConnection conn;
            SqlCommand command;
            
            try
            {
                DirectoryInfo di = new DirectoryInfo(TargetDirectory);
                FileInfo[] FileList = di.GetFiles();

                //!*! it'd be a lot cleaner to move the connection open/close outside the foreach loop...less chatter. 
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

                    //replace table chc_test with real table that will include file_name, project_id, email recipient
                    //I need to use SqlDataReader to populate these needed variables...the select count(*) is too simplistic
                    //!*! NOTE: I need to accommodate the AUG2017 variable from month to month....

                    command = new SqlCommand("select count(*) from dbo.chc_test where [file_type]='" + ProcessFlag + "' and [test column] = '" + fi_name + "'", conn);
                    command.CommandTimeout = 3600;
                    int file_count = (int) command.ExecuteScalar();
                    conn.Close();
                    Console.WriteLine(file_count);
  
                    if (file_count == 1)
                    {   //using UNC path to both move file 
                        Console.WriteLine("let's move a file");
                    } 

  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with File_Checker");
            }

        }
    }


    // Move file from one location to another 
    // This method should be used when moving files within a server (e.g. move from processing directory to proceseed directory)
    public Boolean MoveFile(StreamWriter Log, string FileType, string FromLocation, string ToLocation)
    {
        // First, make sure file does not exist in destination location (move command does not support overwrite)
        if (System.IO.File.Exists(ToLocation))
        {
            Log.WriteLine(FileType.Trim() + " File Exists in Destination Directory - removing it");
            try
            {
                System.IO.File.Delete(ToLocation);
            }
            catch (Exception ex)
            {
                Log.WriteLine("Problem Deleting " + ToLocation);
                Log.WriteLine("Stack Trace:" + ex.StackTrace + Environment.NewLine);
                return false;
            }
        }
        try
        {
            Log.WriteLine("Moving " + FromLocation + " to " + ToLocation);
            File.Move(FromLocation, ToLocation);
            // Once the file is moved, it should no longer be in source location, let's make sure and delete it if it is still there.
            if (System.IO.File.Exists(FromLocation))
            {
                // Now that the file is successfully copied, remove it from original directory
                try
                {
                    Log.WriteLine("Deleting " + FromLocation);
                    System.IO.File.Delete(FromLocation);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.WriteLine("Problem Deleting " + FromLocation);
                    Log.WriteLine("Stack Trace:" + ex.StackTrace + Environment.NewLine);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.WriteLine("Problem Moving " + FromLocation + " to " + ToLocation);
            Log.WriteLine("Stack Trace:" + ex.StackTrace + Environment.NewLine);
            return false;
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data;

namespace survGIS
{
    class DatabaseManager
    {
        SQLiteConnection dbConnection;
        SQLiteCommand command;
        string sqlCommand;
        string dbPath = "C:\\Users\\benja\\Documents\\";//"Z:\\dev\\JobDB\\JobDB\\";
        string dbFilePath;
       
        public string Begin()
        {
            try
            {             
                dbFilePath = dbPath + "jobs.db";

                if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
                {
                    Directory.CreateDirectory(dbFilePath);
                }
                    

                bool firstRun = false;
                if (!System.IO.File.Exists(dbFilePath))
                {
                    SQLiteConnection.CreateFile(dbFilePath);
                    firstRun = true;
                }

                string strCon = string.Format("Data Source={0};", dbFilePath);
                dbConnection = new SQLiteConnection(strCon);
                dbConnection.Open();

                command = dbConnection.CreateCommand();

                if(firstRun)
                {
                    CreateTable("PCVA");
                }
                return strCon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect: " + ex);
                Environment.Exit(1);
                return "";
            }
        }

        public void CreateTable(string tableName)
        {
           
            command.CommandText = "CREATE TABLE IF NOT EXISTS PCVA(" +
                "jobid INTEGER PRIMARY KEY, " +
                "town INTEGER NOT NULL, " +
                "sec INTEGER NOT NULL, " +
                "blk INTEGER NOT NULL, " +
                "lot INTEGER NOT NULL);";
            command.ExecuteNonQuery();
        }
        public void ExecuteQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public void UpdateJob(string tableName, int job)
        {
            command.CommandText = string.Format("SELECT ");
        }
        public bool TableContainsData(string tableName)
        {
            command.CommandText = string.Format("SELECT count(*) FROM {0}", tableName);
            var result = command.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }
        public void NewJob(int jobid, int town, int sec, int blk, int lot)
        {
            if(!TableContainsData("PCVA"))
            {
                jobid = 1;                
                string jobidstring = jobid.ToString();
                town = 2;
                sec = 230;
                blk = 2;
                lot = 412;

                command.CommandText= "INSERT INTO PCVA (jobID, town, sec, blk, lot) "+
                    "VALUES (@jobid, @town, @sec, @blk, @lot)";
                command.Parameters.AddWithValue("@jobid", jobid);
                command.Parameters.AddWithValue("@town", town);
                command.Parameters.AddWithValue("@sec", sec);
                command.Parameters.AddWithValue("@blk", blk);
                command.Parameters.AddWithValue("@lot", lot);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("Row Inserted");
            }
        }
        public ObservableCollection<Job> getTable()
        {
            ObservableCollection<Job> joblist = new ObservableCollection<Job>();
            sqlCommand = string.Format("SELECT JobNo, Town, Page, Block, Lot, ClientName FROM PCVA");
            command.CommandText = sqlCommand;
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                joblist.Add(new Job(rdr.GetInt32(0), 6, GetSafeString(rdr, 2), GetSafeString(rdr, 3), GetSafeString(rdr, 4), GetSafeString(rdr, 5)));
            }

            return joblist;
            
        }

        private static string GetSafeString(SQLiteDataReader rd, int colIdx)
        {
            if (!rd.IsDBNull(colIdx))
                return rd.GetString(colIdx);
            return string.Empty;
        }
      
    }
}

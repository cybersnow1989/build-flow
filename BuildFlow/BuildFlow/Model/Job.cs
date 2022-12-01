using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Java.Util;

namespace BuildFlow.Model
{
    public class Job
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int InvoiceID { get; set; }
        public string JobTitle { get; set; }

        public static Job InsertJob(Job job)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Job>();
                conn.Insert(job);
                return job;
            }
        }

        public static List<Job> GetJobs()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Job>();
                var jobs = conn.Table<Job>().ToList();
                return jobs;
            }
        }

        public static bool Update(Job job)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Job>();
                int rows = conn.Update(job);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Delete(Job job)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Job>();
                int rows = conn.Delete(job);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

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
        public string JobName { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}

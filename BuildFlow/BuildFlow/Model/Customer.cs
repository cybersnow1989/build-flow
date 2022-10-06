using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BuildFlow.Model
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

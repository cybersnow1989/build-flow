using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Android.Provider;

namespace BuildFlow.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

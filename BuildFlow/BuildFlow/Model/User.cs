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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public static User InsertUser(User user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                conn.Insert(user);
                return user;
            }
        }

        public static User GetUserByEmailAndPassword(string email, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                var user = conn.Table<User>().FirstOrDefault(x => x.Email == email && x.Password == password);
                return user;
            }
        }

        public static bool Update(User user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                int rows = conn.Update(user);
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

        public static List<User> GetUsers()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();
                return users;
            }
        }
    }
}

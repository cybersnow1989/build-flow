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

        public static List<Customer> GetCustomers()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();
                var customers = conn.Table<Customer>().ToList();
                return customers;
            }
        }

        public static Customer GetCustomerById(int customerId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();
                Customer customer = conn.Table<Customer>().FirstOrDefault(x => x.ID == customerId);
                return customer;
            }
        }

        public static bool InsertCustomer(Customer customer)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();
                int rows = conn.Insert(customer);
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

        public static bool Update(Customer customer)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();
                int rows = conn.Update(customer);
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

        public static bool Delete(Customer customer)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Customer>();
                int rows = conn.Delete(customer);
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

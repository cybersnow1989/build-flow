using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using BuildFlow.Enums;

namespace BuildFlow.Model
{
    public class Invoice
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int JobID { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public decimal InvoiceTotal { get; set; }
        public DateTime CreatedOn { get; set; }

        public static Invoice InsertInvoice(Invoice invoice)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Invoice>();
                    conn.Insert(invoice);
                    return invoice;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There was a database issue.");
                return invoice;
            }
        }

        public static List<Invoice> GetInvoices()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Invoice>();
                    var invoices = conn.Table<Invoice>().ToList();
                    return invoices;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There was a database issue.");
                return null;
            }
        }

        public static Invoice GetInvoiceByJobID(int jobID)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Invoice>();
                    var invoice = conn.Table<Invoice>().FirstOrDefault(x => x.JobID == jobID);
                    return invoice;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There was a database issue.");
                return null;
            }
        }

        public static bool Update(Invoice invoice)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Invoice>();
                    int rows = conn.Update(invoice);
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
            catch (Exception)
            {
                Console.WriteLine("There was a database issue.");
                return false;
            }
        }

        public static bool Delete(Invoice invoice)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Invoice>();
                    int rows = conn.Delete(invoice);
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
            catch (Exception)
            {
                Console.WriteLine("There was a database issue.");
                return false;
            }
        }
    }
}

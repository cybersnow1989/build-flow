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

        public static bool InsertInvoice(Invoice invoice)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Invoice>();
                int rows = conn.Insert(invoice);
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

        public static List<Invoice> GetInvoices()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Invoice>();
                var invoices = conn.Table<Invoice>().ToList();
                return invoices;
            }
        }

        public static Invoice GetInvoiceByJobID(int jobID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Invoice>();
                var invoice = conn.Table<Invoice>().FirstOrDefault(x => x.JobID == jobID);
                return invoice;
            }
        }

        public static bool Update(Invoice invoice)
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

        public static bool Delete(Invoice invoice)
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
    }
}

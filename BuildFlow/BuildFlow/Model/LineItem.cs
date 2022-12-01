using Android.Speech.Tts;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow.Model
{
    public class LineItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemPrice { get; set; }

        public static List<LineItem> InsertLineItems(List<LineItem> lineItems)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LineItem>();

                foreach (LineItem lineItem in lineItems)
                {
                    conn.Insert(lineItem);
                }

                return lineItems;
            }
        }

        public static List<LineItem> GetLineItemsByInvoiceID(int invoiceID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LineItem>();
                var lineItems = conn.Table<LineItem>().Where(x => x.InvoiceID == invoiceID).ToList();
                return lineItems;
            }
        }

        public static bool DeleteLineItems(List<LineItem> lineItems)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                int deleteCount = 0;
                conn.CreateTable<LineItem>();

                foreach (LineItem lineItem in lineItems)
                {
                    deleteCount += conn.Delete(lineItem);
                }

                return (deleteCount == lineItems.Count);
            }
        }
    }
}

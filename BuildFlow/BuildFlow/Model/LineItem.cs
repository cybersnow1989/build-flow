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

        public static bool InsertLineItems(List<LineItem> lineItems)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                bool success = false;
                conn.CreateTable<LineItem>();

                foreach (LineItem lineItem in lineItems)
                {
                    int rows = conn.Insert(lineItem);
                    if (rows > 0)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }

                return success;
            }
        }
    }
}

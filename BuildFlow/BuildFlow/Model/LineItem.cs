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
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemUnit { get; set; }
        public decimal ItemAmount { get; set; }
        public double ItemPrice { get; set; }
    }
}

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
        public List<LineItem> LineItems { get; set; }
        public double InvoiceTotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RETracker.Models
{
    public class TransEntry
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public DateTime EntryDate { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}

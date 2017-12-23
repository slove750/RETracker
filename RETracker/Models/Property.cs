using System;
using System.Collections.Generic;
using System.Text;

namespace RETracker.Models
{
    public class Property
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string ParcelNo { get; set; }
    }
}

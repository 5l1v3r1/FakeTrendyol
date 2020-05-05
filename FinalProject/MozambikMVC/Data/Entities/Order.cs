using Mozambik.Data;
using Mozambik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public byte Count { get; set; }
        public decimal Price { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public bool isDelivered { get; set; }
        public bool isCanceled { get; set; }
    }
}

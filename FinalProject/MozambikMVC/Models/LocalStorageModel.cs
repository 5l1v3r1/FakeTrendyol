using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Models
{
    public class LocalStorageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Quantity { get; set; }
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class SizeProducts
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<ColorProduct> ColorProducts { get; set; }
        public Color()
        {
            ColorProducts = new HashSet<ColorProduct>();
        }
    }
}

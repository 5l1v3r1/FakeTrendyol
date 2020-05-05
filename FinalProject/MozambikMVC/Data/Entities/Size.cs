using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SizeProducts> SizeProducts { get; set; }

        public Size()
        {
            SizeProducts = new HashSet<SizeProducts>();
        }
    }
}

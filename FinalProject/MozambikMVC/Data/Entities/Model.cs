using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MozambikMVC.Data.Entities
{
    public class Model
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int MarkaID { get; set; }
        public Marka Marka { get; set; }
        public ICollection<Product> Products { get; set; }
        public Model()
        {
            Products = new HashSet<Product>();
        }
    }
}
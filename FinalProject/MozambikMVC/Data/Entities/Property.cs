using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MozambikMVC.Data.Entities
{
    public class Property
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Value { get; set; }

       public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
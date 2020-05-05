using Mozambik.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MozambikMVC.Data.Entities
{
    public class Product :BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte Discount { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<SizeProducts> SizeProducts { get; set; }
        public ICollection<ColorProduct> ColorProducts { get; set; }
        public string Description { get; set; }
        public Model Model { get; set; }
        public int ModelID { get; set; }

        public Product()
        {
            Pictures = new HashSet<Picture>();
            Comments = new HashSet<Comment>();
            Properties = new HashSet<Property>();
            ColorProducts = new HashSet<ColorProduct>();
            SizeProducts = new HashSet<SizeProducts>();
        }
    }
}
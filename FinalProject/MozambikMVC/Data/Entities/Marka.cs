using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MozambikMVC.Data.Entities
{
    public class Marka
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Marka()
        {
            Models = new HashSet<Model>();
        }
    }
}
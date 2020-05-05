using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MozambikMVC.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Marka> Markas{get;set;}
        public int SubMenuID { get; set; }
        public SubMenu SubMenu { get; set; }
        public Category()
        {
            Markas = new HashSet<Marka>();
        }
    }
}
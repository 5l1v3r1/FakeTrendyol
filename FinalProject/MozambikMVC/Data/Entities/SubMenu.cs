using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MozambikMVC.Data.Entities
{
    public class SubMenu
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Category> Categories { get; set; }
        public SubMenu()
        {
            Categories = new HashSet<Category>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class Menu
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<SubMenu> SubMenus { get; set; }
        public Menu()
        {
            SubMenus = new HashSet<SubMenu>();
        }
    }
}

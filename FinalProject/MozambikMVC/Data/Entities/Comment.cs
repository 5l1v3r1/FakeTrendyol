using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        public int StarCount { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}

using Mozambik.Data.Entities;
using MozambikMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.Entities
{
    public class Picture:BaseEntity
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string PhotoUrl { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}

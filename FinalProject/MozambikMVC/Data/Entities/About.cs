using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mozambik.Data.Entities
{
    public class About
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^((\(?\+994\)?)|(0))?\d{2}-\d{3}-\d{2}-\d{2}$")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}

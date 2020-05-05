using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mozambik.Data.Entities
{
    public class Subscriber
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

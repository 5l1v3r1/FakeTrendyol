using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.DeletedEtities
{
    public class DeletedProduct:BaseEntity
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}

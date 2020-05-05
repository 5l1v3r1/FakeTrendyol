using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Data.DeletedEtities
{
    public class BaseEntity
    {
        public DateTime DeletedDate { get; set; } = DateTime.Now;
        public int DeletedId { get; set; }
    }
}

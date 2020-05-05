using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozambik.Data.Entities
{
    public class BaseEntity
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedID { get; set; }
        public int? ModifiedID { get; set; }
    }
}

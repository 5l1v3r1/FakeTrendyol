using MozambikMVC.Data.DeletedEtities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Infrastructure.Extensions
{
    public static class DeletedDates 
    {
        public static void Delete(this BaseEntity entity, int id)
        {
            entity.DeletedDate = DateTime.Now;
            entity.DeletedId = id;
        }
    }
}

using Mozambik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Infrastructure.Extensions
{
    public static class AddCreatedDateAndID
    {
        public static void AddedDate(this BaseEntity entity,int id)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedID = id;
        }
        public static void ModifiedDate(this BaseEntity entity,int id)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedID = id;
        }

    }
}

using Microsoft.AspNetCore.Identity;
using MozambikMVC.Data.Entities;
using System.Collections.Generic;

namespace Mozambik.Data
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public int? DiscountBonus { get; set; }
        public bool IsVipUser { get; set; }
        public string CategoryIDs { get; set; }
        public bool IsAccepted { get; set; }

        public ICollection<Order> Orders { get; set; }
        public AppUser()
        {
            Orders = new HashSet<Order>();
        }

    }
}

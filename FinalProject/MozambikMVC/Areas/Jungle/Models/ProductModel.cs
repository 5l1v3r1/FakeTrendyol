using Microsoft.AspNetCore.Http;
using Mozambik.Data.Entities;
using MozambikMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Areas.Jungle.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public byte Discount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ModelID { get; set; }
        public List<IFormFile> Pictures { get; set; }


    }
}

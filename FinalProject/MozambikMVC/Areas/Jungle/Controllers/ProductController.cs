using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Mozambik.Data;
using Mozambik.Data.Entities;
using MozambikMVC.Areas.Jungle.Models;
using MozambikMVC.Data.DeletedEtities;
using MozambikMVC.Data.Entities;
using MozambikMVC.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Areas.Jungle.Controllers
{
    [Area("Jungle")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        readonly ProductDbContext procuctDb;
        readonly IHostEnvironment hostEnvironment;
        public ProductController(ProductDbContext dbContext, IHostEnvironment host)
        {
            procuctDb = dbContext;
            hostEnvironment = host;
        }
        public IActionResult Index()
        {
            return View(procuctDb.Products.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Menu = new SelectList(procuctDb.Menus.ToList(), "ID", "Name");
            ViewBag.Colors = procuctDb.Colors;
            ViewBag.Sizes = procuctDb.Sizes;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel productmodel, string colors, int[] sizes)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = productmodel.Name,
                    Discount = productmodel.Discount,
                    Price = productmodel.Price,
                    Description= productmodel.Description,
                    ModelID = productmodel.ModelID

                };
                using (procuctDb)
                {
                    procuctDb.Products.Add(product);
                    procuctDb.SaveChanges();

                    var c = colors.Split(',');
                    foreach (var item in c)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            if (!procuctDb.ColorProducts.Any(x=>x.ColorId== int.Parse(item) && x.ProductId == product.Id))
                            {
                                procuctDb.ColorProducts.Add(new ColorProduct
                                {
                                    ColorId = int.Parse(item),
                                    ProductId = product.Id
                                });
                                procuctDb.SaveChanges();

                            }
                        }
                    }


                    foreach (var item in sizes)
                    {
                        procuctDb.SizeProducts.Add(new SizeProducts
                        {
                            SizeId =item,
                            ProductId = product.Id
                        });
                        procuctDb.SaveChanges();
                    }

                    foreach (var file in productmodel.Pictures)
                    {
                        Picture picture = new Picture
                        {
                            PhotoUrl = file.CreateImage(hostEnvironment),
                            ProductID = product.Id
                        };
                        procuctDb.Pictures.Add(picture);
                    }

                    await procuctDb.SaveChangesAsync();
                }
                //Send Email

                return RedirectToAction("Index", "Product", "Jungle");
            }
            ViewBag.ModelId = new SelectList(procuctDb.Models, "Id", "Name");
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Pictures = procuctDb.Pictures.Where(x => x.ProductID == id).ToList();
            ViewBag.Properties = procuctDb.Properties.Where(x => x.ProductId == id).ToList();
            ViewBag.ModelId = new SelectList(procuctDb.Models, "Id", "Name");
            return View(procuctDb.Products.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                var model = procuctDb.Products.Find(id);
                model.Name = productmodel.Name;
                model.Discount = productmodel.Discount;
                model.Price = productmodel.Price;
                model.ModelID = productmodel.ModelID;
                model.ModifiedDate = DateTime.Now;
                procuctDb.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                procuctDb.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Model is not valid");
            }
            ViewBag.Pictures = procuctDb.Pictures.Where(x => x.ProductID == id).ToList();
            ViewBag.Properties = procuctDb.Properties.Where(x => x.ProductId == id).ToList();
            ViewBag.ModelId = new SelectList(procuctDb.Models, "Id", "Name");
            return View(procuctDb.Products.Find(id));
        }

        public IActionResult Delete(int id)
        {
            ViewBag.PicturesCount = procuctDb.Pictures.Where(x => x.ProductID == id).Count();
            ViewBag.PropertiesCount = procuctDb.Properties.Where(x => x.ProductId == id).Count();
            return View(procuctDb.Products.Find(id));
        }
        [HttpPost]
        public IActionResult Delete(int id, Product product,string colors,int[] sizes)
        {
            try
            {
                var item = procuctDb.Products.Find(id);
                var itemPictures = procuctDb.Pictures.Where(x => x.ProductID == id).ToList();
                var properies = procuctDb.Properties.Where(x => x.ProductId == id).ToList();
                procuctDb.Properties.RemoveRange(properies);
                foreach (var itemPicture in itemPictures)
                {
                    ImageExtension.DeleteImage(itemPicture.PhotoUrl,hostEnvironment);
                }
                procuctDb.Pictures.RemoveRange(itemPictures);
                procuctDb.Products.Remove(item);
                procuctDb.SaveChanges();
                DeletedProduct deleted = new DeletedProduct
                {
                    DeletedDate = DateTime.Today,
                    Name = item.Name,
                    DeletedId = item.Id
                };
                procuctDb.DeletedProducts.Add(deleted);
                procuctDb.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return RedirectToAction(nameof(Delete),id);
            }
            
           
        }
        public IActionResult EditProperties(int id)
        {
            ViewData["id"] = id;
            return View(procuctDb.Properties.Where(x => x.ProductId == id).ToList());
        }
        [Route("/RemoveProperty")]
        [HttpPost]
        public bool RemoveProperty(int id)
        {
            var property = procuctDb.Properties.Find(id);
            if (property != null)
            {
                procuctDb.Properties.Remove(property);
                try
                {
                    procuctDb.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [Route("/CreateProperty")]
        [HttpPost]
        public bool CreateProperty(Property property)
        {
            procuctDb.Properties.Add(property);
            try
            {
                procuctDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        [Route("/EditProperty")]
        [HttpPost]
        public bool EditProperty(int id, string name, string value)
        {
            var property = procuctDb.Properties.Find(id);
            if (property != null)
            {
                property.Name = name;
                property.Value = value;
                try
                {
                    procuctDb.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [Route("/AddPropertyForEditPage")]
        public PartialViewResult AddPropertyForEditPage(int data)
        {
            return PartialView("_PropertyCreatePartialView", new Property { ProductId = data });
        }

        [Route("AddProperty")]
        public PartialViewResult AddProperty()
        {
            return PartialView("_PropertyPartialView");
        }

        private bool IsPropertyExist(string propertyName, int productId)
        {
            var data = procuctDb.Properties.FirstOrDefault(x => x.Name == propertyName && x.ProductId == productId);
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult EditImages(int id)
        {
            ViewBag.Id = id;
            return View(procuctDb.Pictures.Where(x => x.ProductID == id).ToList());
        }
        [HttpPost]
        [Route("/EditImage")]
        public string EditImage(int id, IFormFile image)
        {
            var picture = procuctDb.Pictures.Find(id);
            if (picture != null)
            {
                var file = picture.PhotoUrl;
                picture.PhotoUrl = image.CreateImage(hostEnvironment);
                if (file != picture.PhotoUrl)
                    ImageExtension.DeleteImage(file, hostEnvironment);
                procuctDb.SaveChanges();
                return picture.PhotoUrl;
            }
            return null;
        }  
        
        [HttpPost]
        [Route("/RemoveImage")]
        public bool RemoveImage(int id)
        {
            var picture = procuctDb.Pictures.Find(id);
            if (picture != null)
            {
                procuctDb.Pictures.Remove(picture);
                ImageExtension.DeleteImage(picture.PhotoUrl, hostEnvironment);
                procuctDb.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost]
        public IActionResult AddImage(int id,IFormFile picture)
        {
            try
            {
                Picture addedPicture = new Picture
                {
                    ProductID = id,
                    PhotoUrl = ImageExtension.CreateImage(picture, hostEnvironment)
                };
                procuctDb.Pictures.Add(addedPicture);
                procuctDb.SaveChanges();
                ViewBag.Id = id;
                return View("EditImages", procuctDb.Pictures.Where(x => x.ProductID == id).ToList());
            }
            catch
            {

                return View(nameof(EditImages), id);
            }
           
        }
    }
}
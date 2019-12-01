using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Management.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository PR = new ProductRepository(new Models.Context.ApplicationDbContext());
        BrandRepository BR = new BrandRepository(new Models.Context.ApplicationDbContext());
        CategoryRepository CR = new CategoryRepository(new Models.Context.ApplicationDbContext());
        // GET: Management/Product
        public ActionResult Index()
        {
            return View(PR.GetAll());
        }
        public ActionResult Create()
        {
            var year = new List<string>();
            for (int i = 2000; i <= 2020; i++)
            {
                year.Add(i.ToString());
            }
            ViewBag.ModelYear = new SelectList(year);
            ViewBag.Brands = BR.GetAll();
            ViewBag.Categories = CR.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model, HttpPostedFileBase image)
        {
          if (ModelState.IsValid)
            {
                if (image != null)
                {
                    using (var br = new BinaryReader(image.InputStream))
                    {
                        var data = br.ReadBytes(image.ContentLength);
                        model.image = data;
                    }
                    
                }
                PR.Save(model);
                return RedirectToAction("/");
            }
            var year = new List<string>();
            for (int i = 2000; i <= 2020; i++)
            {
                year.Add(i.ToString());
            }
            ViewBag.ModelYear = new SelectList(year);
            ViewBag.Brands = BR.GetAll();
            ViewBag.Categories = CR.GetAll();
            ModelState.AddModelError("", "Model yükleme hatası");
            return View();
            
        }
   
    }
}
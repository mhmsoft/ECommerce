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
        ModelRepository MR = new ModelRepository(new Models.Context.ApplicationDbContext());
        SubModelRepository SMR = new SubModelRepository(new Models.Context.ApplicationDbContext());
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
        public ActionResult Create(Product model, HttpPostedFileBase image1)
        {
          if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    using (var br = new BinaryReader(image1.InputStream))
                    {
                        var data = br.ReadBytes(image1.ContentLength);
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
        public ActionResult Edit(int id)
        {
            var year = new List<string>();
            for (int i = 2000; i <= 2020; i++)
            {
                year.Add(i.ToString());
            }
            ViewBag.ModelYear = new SelectList(year);
            ViewBag.Brands = BR.GetAll();
            ViewBag.Categories = CR.GetAll();
            ViewBag.Models = MR.GetAll();
            ViewBag.SubModels = SMR.GetAll();
          
            return View(PR.Get(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    using (var br = new BinaryReader(image1.InputStream))
                    {
                        var data = br.ReadBytes(image1.ContentLength);
                        model.image = data;
                    }

                }
                // resim yüklenmemişse
                else
                {
                    int id = model.Id;
                    var oldImage = PR.Get(id).image;
                    model.image = oldImage;
                }

                PR.Update(model);
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
        public ActionResult Delete(int id)
        {
            return View(PR.Get(id));
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
                PR.Delete(PR.Get(id));
            return RedirectToAction("/");
        }
   
    }
}
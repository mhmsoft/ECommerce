using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Management.Controllers
{
    public class BrandController : Controller
    {
        BrandRepository BR = new BrandRepository(new Models.Context.ApplicationDbContext());
        // GET: Management/Brand
        public ActionResult Index()
        {
            return View(BR.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand model)
        {
            if (ModelState.IsValid)
                BR.Save(model);
            return RedirectToAction("/");
        }
        public ActionResult Edit(int id)
        {
            return View(BR.Get(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand model)
        {
            if (ModelState.IsValid)
                BR.Update(model);
            return RedirectToAction("/");
        }
        public ActionResult Delete(int id)
        {
            return View(BR.Get(id));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrand(int id)
        {
            if (ModelState.IsValid)
                BR.Delete(BR.Get(id));
            return RedirectToAction("/");
        }

    }
}
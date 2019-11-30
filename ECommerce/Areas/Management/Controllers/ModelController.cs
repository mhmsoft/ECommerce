using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Management.Controllers
{
    public class ModelController : Controller
    {
        ModelRepository MR = new ModelRepository(new Models.Context.ApplicationDbContext());
        BrandRepository BR = new BrandRepository(new Models.Context.ApplicationDbContext());

        public ActionResult Index()
        {
            return View(MR.GetAll());
        }
        public ActionResult Create()
        {
            ViewBag.Brands = BR.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model model)
        {
            if (ModelState.IsValid)
                MR.Save(model);
            return RedirectToAction("/");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Brands = BR.GetAll();
            return View(MR.Get(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Model model)
        {
            if (ModelState.IsValid)
                MR.Update(model);
            return RedirectToAction("/");
        }
        public ActionResult Delete(int id)
        {
            ViewBag.Brands = BR.GetAll();
            return View(MR.Get(id));
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModel(int id)
        {
            if (ModelState.IsValid)
                MR.Delete(MR.Get(id));
            return RedirectToAction("/");
        }
    }
}
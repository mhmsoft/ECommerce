using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Management.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository CR = new CategoryRepository(new Models.Context.ApplicationDbContext());
        // GET: Management/Category
        public ActionResult Index()
        {
            return View(CR.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
                CR.Save(model);
            return RedirectToAction("/");
        }
        public ActionResult Edit(int id)
        {
            return View(CR.Get(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
                CR.Update(model);
            return RedirectToAction("/");
        }
        public ActionResult Delete(int id)
        {
            return View(CR.Get(id));
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            if (ModelState.IsValid)
                CR.Delete(CR.Get(id));
            return RedirectToAction("/");
        }

    }
}
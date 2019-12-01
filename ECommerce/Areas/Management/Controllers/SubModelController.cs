using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Management.Controllers
{
    public class SubModelController : Controller
    {
        SubModelRepository SMR = new SubModelRepository(new Models.Context.ApplicationDbContext());
        ModelRepository MR = new ModelRepository(new Models.Context.ApplicationDbContext());
        BrandRepository BR = new BrandRepository(new Models.Context.ApplicationDbContext());
        // GET: Management/SubModel
        public ActionResult Index()
        {
            return View(SMR.GetAll());
        }
        [HttpPost]
        public ActionResult getModels(int brandId)
        {
           
            return Json(MR.GetAll(brandId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getSubModels(int modelId)
        {

            return Json(SMR.GetAll(modelId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Brands = BR.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubModel model)
        {
            if (ModelState.IsValid)
                SMR.Save(model);
            return RedirectToAction("/");
        }
        public ActionResult Edit(int id)
        {
            int brandCode = SMR.Get(id).Model.Brand.Id;
            ViewBag.Brands = new SelectList(BR.GetAll(), "Id", "Name", brandCode);

            ViewBag.Models = new SelectList(MR.GetAll(), "Id", "Name", SMR.Get(id).modelId);
            return View(SMR.Get(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubModel model)
        {
            if (ModelState.IsValid)
                SMR.Update(model);
            return RedirectToAction("/");
        }
        public ActionResult Delete(int id)
        {
            return View(SMR.Get(id));
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletex(int id)
        {
            if (ModelState.IsValid)
                SMR.Delete(SMR.Get(id));
            return RedirectToAction("/");
        }


    }
}
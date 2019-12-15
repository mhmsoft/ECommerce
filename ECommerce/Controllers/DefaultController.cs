using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using ECommerce.Areas.Management.Models.Entities;

namespace ECommerce.Controllers
{
    public class DefaultController : Controller
    {
        ProductRepository ProductManager = new ProductRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        BrandRepository BrandManager = new BrandRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        public ActionResult Index(int?page,int?brandId)
        {                                                                                       
            int _page = page ?? 1;
            // hER SAYFADA ÜRÜN SAYISI
            int _productPerPage = 2;
            var result = ProductManager.GetAll();
             ViewBag.Cars = BrandManager.GetAll();
            // eğer brandId filtrelenmişse
            if (brandId!=null)
            {
                result = result.Where(x => x.brandId == brandId).ToList();

            }
            return View(result.ToPagedList(_page,_productPerPage));
        }
        // car detail
        public ActionResult productDetail(int id)
        {
            return View(ProductManager.Get(id));
        }
        // Comment Partialini aç
        public ActionResult Comments()
        {
            return PartialView(ProductManager.CommentList());
        }
        // Yeni bir yorum kaydetme
        [HttpPost]
        public void NewComment(comment model)
        {
            model.reviewDate = DateTime.Now;
            ProductManager.CommentSave(model);
        }
    }
}
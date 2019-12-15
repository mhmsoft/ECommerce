using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class DefaultController : Controller
    {
        ProductRepository ProductManager = new ProductRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        public ActionResult Index(int?page)
        {                                                                                       
            int _page = page ?? 1;
            int _productPerPage = 6;
            var result = ProductManager.GetAll();
            return View(result.ToPagedList(_page,_productPerPage));
        }
    }
}
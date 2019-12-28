using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Management.Controllers
{
    public class AdminController : Controller
    {

        RentRepository rentManager = new RentRepository(new Areas.Management.Models.Context.ApplicationDbContext());

        // GET: Management/Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult rentCars()
        {
            return View(rentManager.GetAll());
        }
    }
}
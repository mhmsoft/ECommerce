using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "User")]
    public class AccountController : Controller
    {
        CustomerRepository customerManager = new CustomerRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        public ActionResult Index()
        { 
            if (User.Identity.IsAuthenticated)
            {
                var model = customerManager.GetAll().SingleOrDefault(x=>x.email==User.Identity.Name);
                if (model!=null)
                     return View(model);
            }
            return RedirectToAction("Login","Customer");
            
        }
    }
}
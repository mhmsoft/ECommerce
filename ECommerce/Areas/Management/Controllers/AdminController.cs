using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Services;

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
        // araba kiralamayı onayla
        public ActionResult Ok(int id)
        {
            Rent rent = rentManager.Get(id);
            rent.rentState = rentState.Onaylandı;
            rent.Product.state = rentState.Onaylandı;
            rentManager.Update(rent);

            //mail gönderme
            MailService.Subject = "Araba kiralama Onaylandı";
            MailService.title = "Araba kiralama Onaylandı";
            MailService.Body = " Sayın " +rent.Customer.firstName + " " + rent.Customer.lastName+ " </br>Araba kiralama işleminiz tamamlandı</br>"+rent.rentStartDate +" - " +rent.rentEndDate +" tarihleri arasında aracanızı kullanabilirsiniz";
            string ToEmail = rent.Customer.email;
            MailService.sendEmail(ToEmail);
            return RedirectToAction("rentCars");
        }
        // araba kiralamayı iptal et
        public ActionResult Cancel(int id)
        {
            Rent rent = rentManager.Get(id);
            rent.rentState = rentState.iptal;
            rent.Product.state = rentState.Uygun;
            rentManager.Update(rent);
            return RedirectToAction("rentCars");
        }
        public ActionResult Suspend(int id)
        {
            Rent rent = rentManager.Get(id);
            rent.rentState = rentState.Beklemede;
            rent.Product.state = rentState.Beklemede;
            rentManager.Update(rent);
            return RedirectToAction("rentCars");
        }
        public ActionResult takeIt(int id)
        {
            Rent rent = rentManager.Get(id);
            rent.rentState = rentState.Uygun;
            rent.Product.state = rentState.Uygun;
            rentManager.Update(rent);
            return RedirectToAction("rentCars");
        }

    }
}
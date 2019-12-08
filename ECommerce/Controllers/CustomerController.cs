using ECommerce.Areas.Management.Models.Repositories;
using ECommerce.Areas.Management.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Areas.Management.Models.Entities;

namespace ECommerce.Controllers
{
    public class CustomerController : Controller
    {
        CustomerRepository CustomerManager = new CustomerRepository(new ApplicationDbContext());
        // GET: Customer
        public ActionResult Index()
        {
            return RedirectToAction("Register");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer model)
        {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                if (isExistUser(model.email))
                {
                    status = false;
                    message = "Böyle bir email kayıtlı";
                    ViewBag.Status = status;
                    ViewBag.Message = message;
                    return View();
                }
                model.password = Crypto.Crypto.Hash(model.password);
                model.rePassword = Crypto.Crypto.Hash(model.rePassword);
                // kaydolan müşteriyi default User rolünü atıyoruz.
                model.roleId = 2;
                model.createdDate = DateTime.Now;
                //aktivasyon kodu için guid kullanıoruz
                model.activationCode = Guid.NewGuid().ToString();
                // default olarak mail aktivasyon yapmasın
                model.isMailVerified = false;
                // veritabanına müşteriyi kaydediyoruz.
                CustomerManager.Save(model);
                //email gönder
                var verifyUrl = "/Customer/VerifyAccount/" + model.activationCode;
                Services.MailService.Link= Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                Services.MailService.title = "Öz kardeşler araba kiralama hizmeti";
                Services.MailService.Subject = Services.MailService.title + " sitemizi seçtiğiniz için teşekkür ederiz ";
                Services.MailService.Body=" Hesabınız başarıyla oluşturulmuştur. Hesabınız aktive etmek için aşağıdaki linke tıklayınız" +
                    " <br/><br/> <a href='" + Services.MailService.Link + "'>Doğrulama Linki  </a> ";
                Services.MailService.sendEmail(model.email);
                status = true;
                message = "Kaydınız yapılmıştır. Kayıt aktivasyonu için mailinize bakınız";
                ViewBag.Status = status;
                ViewBag.Message = message;
                return View();
            }

            ModelState.AddModelError("","Bilgileri kontrol ediniz");
            return View();
            
        }

        [NonAction]
        public bool isExistUser(string username)
        {
            var user = CustomerManager.GetAll().Where(a => a.email == username).FirstOrDefault();
            return user == null ? false : true;
        }
    }
}
using ECommerce.Areas.Management.Models.Repositories;
using ECommerce.Areas.Management.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.VM;
using System.Web.Security;

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
        //mail adresinizdeki doğrulmayı yapmak için
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool status = false;
            var result = CustomerManager.GetAll().FirstOrDefault(a => a.activationCode == new Guid(id).ToString());
            if (result != null)
            {
                result.isMailVerified = true;
                CustomerManager.Update(result);
                status = true;
                ViewBag.Status = status;
                ViewBag.Message = "Hesabınız başarıyla aktive edilmiştir. Giriş yapabilirsiniz";
            }
            else
            {
                ViewBag.Status = status;
                ViewBag.Message = "Geçersiz istek";
            }
            return View("Login");
        }
        //login
        public ActionResult Login()
        {
            return View();
        }
        //ip adresini alıyor
        public string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string ReturnUrl)
        {
            string message = "";
            int sayac = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                //tüm müşterilerin içinde kullanıcının emaili varmı?
                Customer _user = CustomerManager.GetAll().FirstOrDefault(x => x.email == login.username);
                //aşama-1-> emaili kayıtlı değilse
                if (_user == null)
                {
                    message = "Böyle bir Email kayıtlı değil";
                    ViewBag.Message = message;
                    ViewBag.Status = status;
                    return View();
                }
                // aşama-2->kullanıcı mail doğrulaması yapmışsa al, yapmamışsa false al.
                bool verify = _user.isMailVerified ?? false;
                if (!verify)
                {
                    message = " Mail doğrulaması yapılmamış";
                    ViewBag.Message = message;
                    ViewBag.Status = status;
                    sayac++;
                    _user.loginAttempt = sayac;
                    CustomerManager.Update(_user);
                    return View();
                }
                // aşama-3->kullanıcı aktif değilse
                if (_user.isActive == false)
                {
                    sayac++;
                    message = "Hesabınız askıya alınmıştır";
                    ViewBag.message = message;
                    _user.loginAttempt = sayac;
                    CustomerManager.Update(_user);
                }
                // form üzerinden girdiğiniz şifre şifreleniyor.
                login.password = Crypto.Crypto.Hash(login.password);
                //aşama-4-> şifreler tutuyorsa
                if (string.Compare(login.password, _user.password) == 0)
                {
                    _user.loginTime = DateTime.Now;
                    _user.loginAttempt = sayac;
                    _user.isActive = true;
                    _user.hostName = GetIp();
                    CustomerManager.Update(_user);

                    Session["username"] = _user.email;
                    // eğer beni hatırla seçilmişse 60 seçilmemişse 10
                    int timeOut = login.rememberMe ? 60 : 10;
                    // form autentication oluşturalım.
                    var ticket = new FormsAuthenticationTicket(login.username, login.rememberMe, timeOut);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeOut);
                    cookie.HttpOnly = true;
                    // Cookie ekleniyor
                    FormsAuthentication.SetAuthCookie("userName", login.rememberMe);
                    Response.Cookies.Add(cookie);

                    // login olan kişi admin ise adminin sayfasına yönlendir
                    if (_user.roleId == 1)
                        return Redirect("~/Management/Category");

                    // return Url yerel bir url ise
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
                // parola yanlışsa
                else
                {
                    sayac++;
                    _user.loginAttempt = sayac;
                    CustomerManager.Update(_user);
                    message = "Giriş yapılamadı.Parola yanlış!";
                }

            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View();
        }

        public ActionResult forgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult forgotPassword(string username)
        {
            bool status = false;
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.Status = status;
                ViewBag.Message = "Eposta adresinizi yazınız";
                return View();
            }
            Customer _user = CustomerManager.GetAll().FirstOrDefault(x => x.email == username);
            //aşama-1-> emaili kayıtlı değilse
            if (_user==null)
            {
                ViewBag.Status = status;
                ViewBag.Message = "Eposta adresiniz kayıtlı değil";
                return View();
            }
            // reset code üretiliyor
           _user.resetCode= Guid.NewGuid().ToString();
            CustomerManager.Update(_user);
             //email gönder
            var resetPasswordUrl = "/Customer/resetPassword/" + _user.resetCode;
            Services.MailService.Link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, resetPasswordUrl);
            Services.MailService.title = "Öz kardeşler araba kiralama hizmeti";
            Services.MailService.Subject = Services.MailService.title + " Parola sıfırlama ";
            Services.MailService.Body = " Talebiniz üzerine parola sıfırlama işlemi tanımlanmıştır. Yeni bir parola belirlemek için aşağıdaki linke tıklayınız" +
                " <br/><br/> <a href='" + Services.MailService.Link + "'>Parola Sıfırlama Linki  </a> ";
            Services.MailService.sendEmail(_user.email);
            status = true;
            ViewBag.Status = status;
            ViewBag.Message = " Parola sıfırlamak için mailinize bakınız";
            return View();
        }
        [HttpGet]
        public ActionResult resetPassword(string id)
        {
            RPassword newResetModel = new RPassword();
            newResetModel.resetcode = id;
            return View(newResetModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult resetPassword(RPassword rp)
        {
            string message = "";
            bool status = false;
            if (ModelState.IsValid)
            {
                Customer _user = CustomerManager.GetAll().FirstOrDefault(x => x.resetCode == rp.resetcode);
                if (_user != null)
                {
                    _user.password = Crypto.Crypto.Hash(rp.password);
                    _user.rePassword = Crypto.Crypto.Hash(rp.rePassword);
                    CustomerManager.Update(_user);
                    status = true;
                    message = "Şifreniz başarıyla değiştirildi";
                }
                else
                    message = "Kullanıcı bulunamadı";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
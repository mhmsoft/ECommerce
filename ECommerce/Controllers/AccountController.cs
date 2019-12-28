using ECommerce.Areas.Management.Models.Entities;
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
        WishListRepository wishListManager = new WishListRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        RentRepository rentManager = new RentRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        public ActionResult Index()
        {
            ViewBag.firstName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).firstName;
            ViewBag.lastName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).lastName;
            ViewBag.phone = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).phone;
            ViewBag.loginTime = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).loginTime;
            @ViewBag.email = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            if (User.Identity.IsAuthenticated)
            {
                var model = customerManager.GetAll().SingleOrDefault(x=>x.email==User.Identity.Name);
                if (model!=null)
                     return View(model);
            }
            return RedirectToAction("Login","Customer");
            
        }
        public ActionResult MyProfile()
        {
            ViewBag.firstName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).firstName;
            ViewBag.lastName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).lastName;
            ViewBag.phone = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).phone;
            ViewBag.loginTime = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).loginTime;
            @ViewBag.email = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            var model = customerManager.GetAll().SingleOrDefault(x => x.email == User.Identity.Name);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(string email,string firstName,string lastName,string address,string city,string phone )
        {
            var customer = customerManager.GetAll().SingleOrDefault(x => x.email == User.Identity.Name);
            string message = "";
            bool status = false;

            customer.firstName = firstName;
            customer.lastName = lastName;
            customer.address = address;
            customer.city = city;
            customer.phone = phone;
         
                customerManager.Update(customer);
                status = true;
                message = "değişiklikler kaydedildi";
          
            ViewBag.Status = status;
            ViewBag.Message = message;
            ViewBag.firstName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).firstName;
            ViewBag.lastName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).lastName;
            ViewBag.phone = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).phone;
            ViewBag.loginTime = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).loginTime;
            @ViewBag.email = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            return View(customer);
           
        }
        public ActionResult changePassword()
        {
            ViewBag.firstName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).firstName;
            ViewBag.lastName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).lastName;
            ViewBag.phone = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).phone;
            ViewBag.loginTime = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).loginTime;
            @ViewBag.email = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            var model = customerManager.GetAll().SingleOrDefault(x => x.email == User.Identity.Name);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult changePassword(string email,string oldPassword,string newPassword)
        {
            string message = "";
            bool status = false;
            var model = customerManager.GetAll().SingleOrDefault(x => x.email == User.Identity.Name);
            // veritabanındaki kayıtlı şifre ile girdiğin eski şifre birbirini tutyorsa
            if (string.Compare(model.password, Crypto.Crypto.Hash(oldPassword))==0)
            {
                model.password = Crypto.Crypto.Hash(newPassword);
                model.rePassword=Crypto.Crypto.Hash(newPassword);
                customerManager.Update(model);
                status = true;
                message = "Şifreniz değiştirildi";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View(model);
        }
        public string addWish(int productId)
        {
            //kullanıcı login olmuşsa
            if(User.Identity.IsAuthenticated)
            {
                int customerId = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).customerId;
                WishList newWish = new WishList()
                {
                    customerId = customerId,
                    productId = productId
                };
                // eğer wishList içerisinde aynı adam aynı ürünü daha önce beğenmişse
               if(!wishListManager.GetAll().Exists(x=>x.productId==productId && x.customerId==customerId))
                   wishListManager.Save(newWish);

                return "Beğenildi";
            }
            return "Beğeni yapmak için üye olunuz";
        }
        public ActionResult MyWishList()
        {
            
            int customerId = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).customerId;
            ViewBag.firstName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).firstName;
            ViewBag.lastName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).lastName;
            ViewBag.phone = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).phone;
            ViewBag.loginTime= customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).loginTime;
            @ViewBag.email= customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            var model = wishListManager.GetAll().Where(x => x.customerId == customerId).ToList();
            return View(model);
        }
        public ActionResult MyRents()
        {

            int customerId = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).customerId;
            ViewBag.firstName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).firstName;
            ViewBag.lastName = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).lastName;
            ViewBag.phone = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).phone;
            ViewBag.loginTime = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).loginTime;
            @ViewBag.email = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            var model = rentManager.GetAll().Where(x => x.custId == customerId).ToList();
            return View(model);
        }
        // beğenilen araçlardan istenileni silme
        [HttpPost]
        public bool deleteWish(int Id)
        {
            if (Id != null)
            {
                wishListManager.Delete(wishListManager.Get(Id));
                return true;
            }
            return false;
        }
        // kiralanan araçlardan istenileni silme
        [HttpPost]
        public bool deleteRent(int Id)
        {
            if (Id != null)
            {
                rentManager.Delete(rentManager.Get(Id));
                return true;
            }
            return false;
        }

    }
}
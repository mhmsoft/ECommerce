using ECommerce.Areas.Management.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.VM;

namespace ECommerce.Controllers
{
    public class DefaultController : Controller
    {
        ProductRepository ProductManager = new ProductRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        BrandRepository BrandManager = new BrandRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        RentRepository rentManager = new RentRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        CustomerRepository customerManager = new CustomerRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        public ActionResult BestRents()
        {
            return PartialView(rentManager.bestRents());
        }

        public ActionResult Index(int?page,int?brandId)
        {                                                                                       
            int _page = page ?? 1;
            // hER SAYFADA ÜRÜN SAYISI
            int _productPerPage = 2;
            var result = ProductManager.GetAll();
             ViewBag.Cars = BrandManager.GetAll();
            ViewBag.brandId = brandId;
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
        public string NewComment(comment model)
        {
            model.reviewDate = DateTime.Now;
            ProductManager.CommentSave(model);
            return "yorum kaydedildi";
        }
        public ActionResult newRent(int Id)
        {
            int customerId=0;
            Product choiceProduct = ProductManager.Get(Id);
            //Customer customer = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name);
          
            Rent newRent = new Rent
            {
                Product = choiceProduct,
                productId=choiceProduct.Id,
                rentStartDate = DateTime.Now.Date,
                rentEndDate = DateTime.Now.Date
               
            };

            
            return View(newRent);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult newRent(Rent model)
        {
            bool status = false;
            string message = "";

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Customer");
               rentState checkRentState = ProductManager.Get(model.productId).state;
            if (checkRentState == rentState.Uygun)
             {
                // rent kaydetme
                int customerId = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).customerId;
                string email = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
                model.custId = customerId;
                model.rentState = rentState.Beklemede;
                // arabanın durumunu beklemeye alacağız
                Product car = ProductManager.Get(model.productId);
                car.state = rentState.Beklemede;
                ProductManager.Update(car);
                //---------
                rentManager.Save(model);

                //email gönder
                var url = "/Account/MyRents/";
                Services.MailService.Link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
                Services.MailService.title = "Öz kardeşler araba kiralama hizmeti";
                Services.MailService.Subject = Services.MailService.title + " sitemizi seçtiğiniz için teşekkür ederiz ";
                Services.MailService.Body = " Araba kiralama işleminin başarılı bir şekilde tamamlandı.";
                Services.MailService.sendEmail(email);
                status = true;
                message = "Araba kiralama işleminin başarılı bir şekilde tamamlandı";
                ViewBag.Status = status;
                ViewBag.Message = message;

                model.Product = ProductManager.Get(model.productId);
                return View(model);
            }
            else
            {
                status = false;
                message = "o tarihlerde uygun değil";
                ViewBag.Status = status;
                ViewBag.Message = message;

                model.Product = ProductManager.Get(model.productId);
                return View(model);
            }
          
        }
       
    }
}
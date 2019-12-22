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
        RentRepository rentManager = new RentRepository(new Areas.Management.Models.Context.ApplicationDbContext());
        CustomerRepository customerManager = new CustomerRepository(new Areas.Management.Models.Context.ApplicationDbContext());
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
        public string NewComment(comment model)
        {
            model.reviewDate = DateTime.Now;
            ProductManager.CommentSave(model);
            return "yorum kaydedildi";
        }
        public ActionResult newRent(int productId)
        {
            Product choiceProduct = ProductManager.Get(productId);
            Customer customer = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name);
            Rent newRent = new Rent
            {
                Product = choiceProduct,
                Customer=customer
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
            // rent kaydetme
            int customerId = customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).customerId;
            string email= customerManager.GetAll().FirstOrDefault(x => x.email == User.Identity.Name).email;
            model.customerId = customerId;
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

            return View();
        }
    }
}
using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Interface;
using ECommerce.Areas.Management.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationDbContext db;
        public ProductRepository(ApplicationDbContext _db)
        {
            db = _db; 
        }

        public void Delete(Product entity)
        {
            db.Product.Remove(entity);
            db.SaveChanges();
        }

        public Product Get(int Id)
        {
            return db.Product.FirstOrDefault(x=>x.Id==Id);
        }

        public List<Product> GetAll()
        {
            return db.Product.ToList();
        }

        public void Save(Product entity)
        {
            db.Product.Add(entity);
            db.SaveChanges();
        }

        public void Update(Product entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
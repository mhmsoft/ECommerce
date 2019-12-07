using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Interface;
using ECommerce.Areas.Management.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Entity;

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

        public List<Product> GetAll(Expression<Func<Product, bool>> where)
        {
            return db.Product.Where(where).ToList();
        }

        public void Save(Product entity)
        {
            db.Product.Add(entity);
            db.SaveChanges();
        }

        public void Update(Product entity)
        {
            Product old = Get(entity.Id);
            db.Entry(old).State = EntityState.Detached;
            
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
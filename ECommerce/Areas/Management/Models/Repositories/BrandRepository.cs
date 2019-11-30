using ECommerce.Areas.Management.Models.Context;
using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ECommerce.Areas.Management.Models.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {
        private ApplicationDbContext db;
        public BrandRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Delete(Brand entity)
        {
            db.Brand.Remove(entity);
            db.SaveChanges();
        }

        public Brand Get(int Id)
        {
            return db.Brand.FirstOrDefault(x=>x.Id==Id);
        }

        public List<Brand> GetAll()
        {
            return db.Brand.ToList();
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> where)
        {
            return db.Brand.Where(where).ToList();
        }

        public void Save(Brand entity)
        {
            db.Brand.Add(entity);
            db.SaveChanges();
        }

        public void Update(Brand entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
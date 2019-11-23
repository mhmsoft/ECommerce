using ECommerce.Areas.Management.Models.Context;
using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Delete(Category entity)
        {
            db.Category.Remove(entity);
            db.SaveChanges();
        }

        public Category Get(int Id)
        {
            return db.Category.FirstOrDefault(x=>x.Id==Id);
        }

        public List<Category> GetAll()
        {
            return db.Category.ToList();
        }

        public void Save(Category entity)
        {
            db.Category.Add(entity);
            db.SaveChanges();
        }

        public void Update(Category entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
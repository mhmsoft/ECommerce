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
    public class ModelRepository:IRepository<Model>
    {
        private ApplicationDbContext db;
        public ModelRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Delete(Model entity)
        {
            db.Model.Remove(entity);
            db.SaveChanges();
        }

        public Model Get(int Id)
        {
            return db.Model.FirstOrDefault(x => x.Id == Id);
        }

        public List<Model> GetAll()
        {
            return db.Model.ToList(); 
        }

        public List<Model> GetAll(Expression<Func<Model, bool>> where)
        {
            return db.Model.Where(where).ToList();
        }

        public List<Model> GetAll(int brandId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Model.Where(x => x.brandId == brandId).ToList();
        }

        public void Save(Model entity)
        {
            db.Model.Add(entity);
            db.SaveChanges();
        }

        public void Update(Model entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
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
    public class SubModelRepository : IRepository<SubModel>
    {
        private ApplicationDbContext db;
        public SubModelRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public void Delete(SubModel entity)
        {
            db.SubModel.Remove(entity);
            db.SaveChanges();
        }

        public SubModel Get(int Id)
        {
            return db.SubModel.Find(Id);
        }

        public List<SubModel> GetAll()
        {
            return db.SubModel.ToList();
        }
        public List<SubModel> GetAll(int modelId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SubModel.Where(x=>x.modelId==modelId).ToList();
        }

        public List<SubModel> GetAll(Expression<Func<SubModel, bool>> where)
        {
            return db.SubModel.Where(where).ToList();
        }
       

        public void Save(SubModel entity)
        {
            db.SubModel.Add(entity);
            db.SaveChanges();
        }

        public void Update(SubModel entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

    }
}
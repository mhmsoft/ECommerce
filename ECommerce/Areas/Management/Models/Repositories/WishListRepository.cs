using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Areas.Management.Models.Context;
using ECommerce.Areas.Management.Models.Interface;
using ECommerce.Areas.Management.Models.Entities;
using System.Linq.Expressions;

namespace ECommerce.Areas.Management.Models.Repositories
{
    public class WishListRepository:IRepository<WishList>
    {
        private ApplicationDbContext db;
        public WishListRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Delete(WishList entity)
        {
            db.WishList.Remove(entity);
            db.SaveChanges();
        }

        public WishList Get(int Id)
        {
            return db.WishList.Find(Id);
        }

        public List<WishList> GetAll()
        {
            return db.WishList.ToList();
        }

        public List<WishList> GetAll(Expression<Func<WishList, bool>> where)
        {
           return db.WishList.Where(where).ToList();
        }

        public void Save(WishList entity)
        {
            db.WishList.Add(entity);
            db.SaveChanges();
        }

        public void Update(WishList entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
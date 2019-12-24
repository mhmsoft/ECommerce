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
    public class RentRepository:IRepository<Rent>
    {
        private ApplicationDbContext db;
        public RentRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Delete(Rent entity)
        {
            db.Rent.Remove(entity);
            db.SaveChanges();
         }

        public Rent Get(int Id)
        {
            return db.Rent.Find(Id);
        }

        public List<Rent> GetAll()
        {
            return db.Rent.Include("Customer").ToList();
        }

        public List<Rent> GetAll(Expression<Func<Rent, bool>> where)
        {
            return db.Rent.Where(where).ToList();
        }

        public void Save(Rent entity)
        {
            db.Rent.Add(entity);
            db.SaveChanges();
        }

        public void Update(Rent entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }
    }
}
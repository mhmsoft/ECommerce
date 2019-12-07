using ECommerce.Areas.Management.Models.Entities;
using ECommerce.Areas.Management.Models.Interface;
using ECommerce.Areas.Management.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ECommerce.Areas.Management.Models.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        ApplicationDbContext db;
        public CustomerRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Delete(Customer entity)
        {
            db.Customer.Remove(entity);
            db.SaveChanges();
        }

        public Customer Get(int Id)
        {
            return db.Customer.Find(Id);
        }

        public List<Customer> GetAll()
        {
            return db.Customer.ToList();
        }

        public List<Customer> GetAll(Expression<Func<Customer, bool>> where)
        {
            return db.Customer.Where(where).ToList();
        }

        public void Save(Customer entity)
        {
            db.Customer.Add(entity);
            db.SaveChanges();
        }

        public void Update(Customer entity)
        {
            Customer oldCustomer = Get(entity.customerId);
            db.Entry(oldCustomer).State = System.Data.Entity.EntityState.Detached;
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
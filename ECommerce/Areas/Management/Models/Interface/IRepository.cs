using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ECommerce.Areas.Management.Models.Interface
{
    interface IRepository<T> where T:class
    {
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int Id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T,bool>> where);
    }
}

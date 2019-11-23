using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Areas.Management.Models.Interface
{
    interface IRepository<T> where T:BaseEntity
    {
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int Id);
        List<T> GetAll();
    }
}

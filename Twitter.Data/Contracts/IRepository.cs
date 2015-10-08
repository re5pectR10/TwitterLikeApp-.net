using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Data.Contracts
{
    public interface IRepository<T> where T : class 
    {
        IQueryable<T> All();

        T GetById(object id);

        T Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void SaveChanges(); 
    }
}

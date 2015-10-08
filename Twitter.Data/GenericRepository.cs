using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Contracts;

namespace Twitter.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;

        private readonly IDbSet<T> set;

        public GenericRepository()
            : this(new TwitterDbContext())
        {
        }

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public T GetById(object id)
        {
            return this.set.Find(id);
        }

        public T Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
            return entity;
        }

        public T Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
            return entity;
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}

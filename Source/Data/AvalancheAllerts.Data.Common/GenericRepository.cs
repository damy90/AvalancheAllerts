using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Data.Common
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class GenericRepository<T, TKey> : IDbGenericRepository<T, TKey>
        where T : class
    {
        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private readonly IDbSet<T> DbSet;

        private readonly DbContext Context;

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public IQueryable<T> All()
        {
            return this.DbSet;
        }

        public T GetById(TKey id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void HardDelete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }
    }
}

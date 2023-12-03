//using Hospital.Repositories.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hospital.Repositories.Implementation
//{
//    public class GenricRepository<T> : IDisposable,IGenericRepository<T> where T : class
//    {
//        private readonly ApplicationDbContext _context;
//        internal DbSet<T> dbset;

//        public GenricRepository(ApplicationDbContext context)
//        {
//            _context = context;
//            dbset = _context.Set<T>;
//        }
//        public void Add(T entity)
//        {
//            dbset.Add(entity);
//        }

//        public async Task<T> AddAsync(T entity)
//        {
//            dbset.Add(entity);
//            return entity;
//        }

//        public void Delete(T entity)
//        {
//            if(_context.Entry(entity).State == EntityState.Detached)
//            {
//                dbset.Attach(entity);
//            }
//            dbset.Remove(entity);
//        }

//        public Task<T> DeleteAsync(T entity)
//        {
//            if (_context.Entry(entity).State == EntityState.Detached)
//            {
//                dbset.Attach(entity);
//            }
//            dbset.Remove(entity);
//            return entity;
//        }
//        private bool disposed = false;

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        private void dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if(disposing)
//                {
//                    _context.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
//        {
//            IQueryable<T> query = dbset;
//            if (filter != null)
//            {
//                query = query.Where(filter);
//            }
//            foreach(var includeProperty in 
//                includeProperties.Split(new char[] (','),StringSplitOptions.RemoveEmptyEntries))
//            {
//                query = query.Include(includeProperty);
//            }
//            if (orderBy != null)
//            {
//                return orderBy(query).ToList();
//            }
//            else
//            {
//                return query.ToList();
//            }
//        }

//        public T GetById(object id)
//        {
//            return dbset.Find(id);
//        }
//        public async Task<T> GetByIdAsync(object id)
//        {
//            return await dbset.FindAsync(id);
//        }
//        public void Update(T entity)
//        {
//            dbset.Attach(entity);
//            _context.Entry(entity).State= EntityState.Modified;
//        }

//        public Task<T> UpdateAsync(T entity)
//        {
//            dbset.Attach(entity);
//            _context.Entry(entity).State = EntityState.Modified;
//            return entity;
//        }


//    }
//}
using Hospital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Implementation
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return entity;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<T>.DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

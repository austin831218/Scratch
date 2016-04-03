using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using OAuth2.Common;

namespace OAuth2.Data.Repository
{
    public interface IAsyncRepository<T>
        where T : class
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate = null);
        void Add(T order);
        void Remove(T order);
        Task SaveChangesAsync();
    }

    public class RepositoryBase<T> : IDisposable, IAsyncRepository<T>
        where T : class
    {
        protected IUnitOfWork Context
        {
            get;
            private set;
        }
        public RepositoryBase(IUnitOfWork context)
        {
            Context = context;
        }
        public Task<IQueryable<T>> GetAllAsync()
        {
            return AsyncHelper.RunAsynchronously<IQueryable<T>>(() =>
            {
                return this.Context.GetAll<T>();
            });
        }
        public Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return AsyncHelper.RunAsynchronously<T>(() =>
            {
                return this.Context.GetAll<T>().FirstOrDefault(predicate);
            });
        }
        public void Add(T order)
        {
            this.Context.Add(order);
        }
        public void Remove(T order)
        {
            this.Context.Delete(order);
        }
        public Task SaveChangesAsync()
        {
            return AsyncHelper.RunAsynchronously(() =>
            {
                this.Context.SaveChanges();
            });
        }
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate = null)
        {
            return AsyncHelper.RunAsynchronously<bool>(() =>
            {
                return (predicate == null)
                ? Context.GetAll<T>().Any()
                : Context.GetAll<T>().Any(predicate);
            });
        }
        public void Dispose()
        {
            this.Context = null;
        }
    }
}

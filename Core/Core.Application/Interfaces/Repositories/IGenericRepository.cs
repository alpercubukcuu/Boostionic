using Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(
         Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        IQueryable<T> GetAll(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] include);
        T GetSingle(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        Task<T> GetSingleAsync(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        Task<T> GetSingleAsync(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] include);

        bool GetAny(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<bool> GetAnyAsync(
         Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);


        #region Add Functions
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entity);
        #endregion

        #region Update Functions
        Task<T> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(List<T> entities);
        Task<T> UpdatePropertyAsync(T entity);
        #endregion

        #region Delete Functions
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(Guid Id);
        Task<bool> DeleteAllAsync(List<T> entities);
        Task<bool> DeleteAllAsync();
        #endregion
    }
}

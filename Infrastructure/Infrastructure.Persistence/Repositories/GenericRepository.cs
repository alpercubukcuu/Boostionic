using Core.Application.Interfaces.Repositories;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _context;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
        }

        public DbSet<T> table => _context.Set<T>();

        public IQueryable<T> GetAll(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderby != null) return orderby(query);
            else return query;

        }
        public T GetSingle(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderby != null) return orderby(query).FirstOrDefault();
            else return query.FirstOrDefault();

        }

        public async Task<T> GetSingleAsync(
              Expression<Func<T, bool>> predicate = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
              bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderby != null)
                return await orderby(query).FirstOrDefaultAsync();
            else
                return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleAsync(
              Expression<Func<T, bool>> predicate = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
              params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] include)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (include != null)
            {
                foreach (var inc in include)
                {
                    query = inc(query);
                }

            }
            if (predicate != null) query = query.Where(predicate);
            if (orderby != null)
                return await orderby(query).FirstOrDefaultAsync();
            else
                return await query.FirstOrDefaultAsync();
        }


        public bool GetAny(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderby != null) return orderby(query).Any();
            else return query.Any();

        }
        public async Task<bool> GetAnyAsync(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderby != null) return orderby(query).Any();
            else return query.Any();

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] include)
        {
            IQueryable<T> query = _context.Set<T>();
            query = query.AsNoTracking();
            if (include != null)
            {
                foreach (var inc in include)
                {
                    query = inc(query);
                }

            }

            if (predicate != null) query = query.Where(predicate);
            if (orderby != null) return orderby(query);
            else return query;
        }


        public async Task<T> AddAsync(T entity)
        {
            try
            {
                if (entity is null) throw new ArgumentNullException(nameof(entity), "Entity degeri bos olamaz!");
                EntityEntry<T> addResult = await table.AddAsync(entity);
                await _context.SaveChangesAsync();
                return addResult.Entity;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
            }
        }
        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            try
            {
                if (entities.Count == 0) throw new ArgumentNullException(nameof(entities), "Liste bos olamaz!");
                await table.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
            }
        }
        public async Task<bool> DeleteAllAsync()
        {
            try
            {
                if (table.Any())
                {
                    table.RemoveRange(table);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (DbUpdateException exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAllAsync(List<T> entities)
        {
            try
            {
                if (entities.Count == 0) throw new ArgumentNullException(nameof(entities), "Liste bos olamaz!");
                table.RemoveRange(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                if (entity is null) throw new ArgumentNullException(nameof(entity), "Entity degeri bos olamaz!");
                table.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteByIdAsync(Guid Id)
        {
            try
            {
                var deleteEntity = table.FirstOrDefault(x => x.Id == Id);
                if (deleteEntity is null) throw new ArgumentNullException(nameof(deleteEntity), "Entity degeri bos olamaz!");
                table.Remove(deleteEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity is null) throw new ArgumentNullException(nameof(entity), "Entity degeri bos olamaz!");
                EntityEntry<T> updateResult = table.Update(entity);
                await _context.SaveChangesAsync();
                return updateResult.Entity;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
            }
        }
        public async Task<bool> UpdateRangeAsync(List<T> entities)
        {
            try
            {
                if (entities is null) throw new ArgumentNullException(nameof(entities), "Liste bos olamaz!");
                table.UpdateRange(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
            }
        }
        public async Task<T> UpdatePropertyAsync(T entity)
        {
            try
            {
                if (entity is null) throw new ArgumentNullException(nameof(entity), "Entity degeri bos olamaz!");

                if (entity.Id == null) throw new ArgumentNullException(nameof(entity), "Entity degeri bos olamaz!");

                EntityEntry entry = this.table.Entry(entity);
                if (entry != null)
                {
                    entity.GetType().GetProperties().ToList()?.ForEach(p =>
                    {
                        object value = p.GetValue(entity);
                        if (value != null && !value.Equals(null))
                        {
                            PropertyEntry entryProp = entry.Properties.Where(x => x.Metadata.GetColumnName() == p.Name).SingleOrDefault();
                            if (entryProp != null && !entryProp.Metadata.IsKey())
                            {
                                entryProp.CurrentValue = p.GetValue(entity);
                                entryProp.IsModified = true;

                            }
                        }
                    });

                    int sevStatus = await _context.SaveChangesAsync();
                    this.table.Entry(entity).State = EntityState.Detached; 

                    if (sevStatus > 0)
                        return entity;
                    else
                        return null;
                }

                return null;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException != null ? exception.InnerException.Message : exception.Message);
            }
        }

       
    }
}

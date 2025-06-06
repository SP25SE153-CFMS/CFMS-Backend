﻿using CFMS.Domain.Interfaces;
using CFMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CFMS.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected CfmsDbContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(CfmsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "",
           int? pageIndex = null,
           int? pageSize = null,
           bool noTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();
        }

        public virtual IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           int? pageIndex = null,
           int? pageSize = null,
           bool noTracking = false,
           params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();
        }

        public virtual IEnumerable<TEntity> GetIncludeMultiLayer(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int? pageIndex = null,
            int? pageSize = null,
            bool noTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null) return;
            _dbSet.Add(entity);
        }

        public virtual bool Delete(object id)
        {
            TEntity entityToDelete = GetByID(id);
            if (entityToDelete == null) return false;
            Delete(entityToDelete);
            return true;
        }

        public virtual bool Update(object id, TEntity entityUpdate)
        {
            if (entityUpdate == null) return false;

            var entity = GetByID(id);
            if (entity == null) return false;

            _context.Entry(entity).CurrentValues.SetValues(entityUpdate);
            _context.Entry(entity).State = EntityState.Modified;

            return true;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            var trackedEntities = _context.ChangeTracker.Entries<TEntity>().ToList();
            foreach (var trackedEntity in trackedEntities)
            {
                trackedEntity.State = EntityState.Detached;
            }
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual bool InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) return false;

            _dbSet.AddRange(entities);
            return true;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null) return;
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateOrInsertAsync(TEntity entity)
        {
            if (entity == null) return;

            var keyName = _context.Model
                .FindEntityType(typeof(TEntity))
                ?.FindPrimaryKey()
                ?.Properties
                ?.Select(p => p.Name)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(keyName)) return;
            var keyValue = _context.Entry(entity).Property(keyName).CurrentValue;
            bool isInsert = keyValue == null || keyValue.Equals(Guid.Empty) || keyValue.Equals(0);

            if (!isInsert)
            {
                var trackedEntities = _context.ChangeTracker.Entries<TEntity>().ToList();
                foreach (var trackedEntity in trackedEntities)
                {
                    trackedEntity.State = EntityState.Detached;
                }
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                if (entity == null) return;
                _dbSet.Add(entity);
            }
        }

        public virtual void UpdateWithoutDetach(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}

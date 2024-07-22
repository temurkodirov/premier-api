﻿using System.Linq.Expressions;

namespace FSSEstate.Repository.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IQueryable<T>> includeFunc = null,
            Expression<Func<T, object>> orderBy = null,
            bool isOrderByDescending = false,
            CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllByQueryAsync(
           Expression<Func<T, bool>> expression,
           Func<IQueryable<T>, IQueryable<T>> includeFunc = null,
           Expression<Func<T, object>> orderBy = null,
           bool isOrderByDescending = false,
           CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}

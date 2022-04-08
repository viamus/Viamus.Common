namespace Viamus.Common.Library.Repository;

using System.Linq.Expressions;
using Viamus.Common.Library.Entities;

/// <summary>
/// Abstraction for repository pattern implementation
/// </summary>
/// <typeparam name="TEntity">Entity type owned by this repository</typeparam>
public interface IRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Add a new object to the repository
    /// </summary>
    /// <param name="entity">Object to be added</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task AddAsync
    (
        TEntity entity, 
        CancellationToken ct = default
    );

    /// <summary>
    /// Add/Update a object that already exists in the entity
    /// </summary>
    /// <param name="filter">Expression to query the object that must be updated</param>
    /// <param name="entity">Object to be added/updated</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task UpdateAsync
    (
        Expression<Func<TEntity, bool>> filter,
        TEntity entity,
        CancellationToken ct = default
    );

    /// <summary>
    /// Get object by its identifier
    /// </summary>
    /// <param name="id">Object id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<TEntity> GetAsync
    (
        Guid id, 
        CancellationToken ct = default
    );

    /// <summary>
    /// Get object by a expression query filter
    /// </summary>
    /// <param name="filter">Expression to query the object that must be returned</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAsync
    (
        Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default
    );
}

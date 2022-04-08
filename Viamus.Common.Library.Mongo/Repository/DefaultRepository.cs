namespace Viamus.Common.Library.Mongo.Repository;

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Viamus.Common.Library.Entities;
using Viamus.Common.Library.Mongo.Context;
using Viamus.Common.Library.Repository;

/// <summary>
/// Implementation of the IRepository<T> for Mongodb
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class DefaultRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly IMongoContext _context;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="context">MongoContext</param>
    public DefaultRepository(IMongoContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Add a entity to your collection
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task AddAsync
    (
        TEntity entity,
        CancellationToken ct = default
    )
    {
        ct.ThrowIfCancellationRequested();

        await _context.GetCollection<TEntity>().InsertOneAsync
        (
            entity, 
            cancellationToken: ct
        );
    }

    /// <summary>
    /// Add or Update a to your collection
    /// </summary>
    /// <param name="filter">Expression to query the entity that must be updated</param>
    /// <param name="entity">Entity</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task UpdateAsync
    (
        Expression<Func<TEntity, bool>> filter,
        TEntity entity,
        CancellationToken ct = default
    )
    {
        ct.ThrowIfCancellationRequested();

        await _context.GetCollection<TEntity>().ReplaceOneAsync
        (
             filter,
             entity,
             new ReplaceOptions
             {
                 IsUpsert = true
             },
             ct
         );
    }

    /// <summary>
    /// Get a entity by id from collection
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetAsync
    (
        Guid id,
        CancellationToken ct = default
    )
    {
        ct.ThrowIfCancellationRequested();

        var queryResut = await _context.GetCollection<TEntity>().FindAsync
        (
            f => 
                f.Id == id, 
                cancellationToken: ct
        );

        return await queryResut.FirstOrDefaultAsync(ct);
    }

    /// <summary>
    /// Get a entity by filter from collection
    /// </summary>
    /// <param name="filter">Expression to query the object that must be returned</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> GetAsync
    (
        Expression<Func<TEntity, bool>> filter,
        CancellationToken ct = default
    )
    {
        ct.ThrowIfCancellationRequested();

        var result = await _context.GetCollection<TEntity>().FindAsync
        (
            filter, 
            cancellationToken: ct
        );

        return await result.ToListAsync(ct);
    }
}
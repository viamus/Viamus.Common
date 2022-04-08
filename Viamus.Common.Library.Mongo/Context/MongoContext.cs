namespace Viamus.Common.Library.Mongo.Context;

using MongoDB.Driver;
using Viamus.Common.Library.Mongo.Attributes;
using Viamus.Common.Library.Mongo.Entities;

/// <summary>
/// MongoContext abstraction, contains the must method
/// </summary>
public interface IMongoContext
{
    /// <summary>
    /// Returns one entity collection
    /// </summary>
    /// <typeparam name="T">Entity collection type</typeparam>
    /// <param name="name">Name of the collection, if not null will return the collection name or else will return with the Entity name</param>
    /// <returns>IMongoCollection T</returns>
    IMongoCollection<T> GetCollection<T>(string? name = null);

}

/// <summary>
/// MongoContext implementation. It contains all the mongo collection access. 
/// </summary>
public sealed class MongoContext : IMongoContext
{
    private readonly IMongoConnection _mongoConnection;

    private readonly IMongoEntityConfig _configs;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="mongoConnection">MongoConnection, transportation layer to the mongo server</param>
    /// <param name="configs">Indexes of the collections stored at mongo</param>
    public MongoContext
    (
        IMongoConnection mongoConnection,
        IMongoEntityConfig configs
    )
    {
        _mongoConnection = mongoConnection;
        _configs = configs;
        Configure();
    }

    #region Private Methods

    private void Configure()
    {
        var entitiesConfig = _configs.GetConfigs();

        if (entitiesConfig is not null && entitiesConfig.Any())
        {
            foreach (var entity in entitiesConfig)
            {
                entity.CreateIndexes(this);
            }
        }
    }

    private IMongoCollection<T> GetCollection<T>(IMongoDatabase database)
    {
        var attrs = typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), false).OfType<CollectionNameAttribute>().FirstOrDefault();

        var collectionName = attrs?.Name ?? typeof(T).Name;

        return database.GetCollection<T>(collectionName);
    }

    #endregion


    /// <summary>
    /// Returns one entity collection
    /// </summary>
    /// <typeparam name="T">Entity collection type</typeparam>
    /// <param name="name">Name of the collection, if not null will return the collection name or else will return with the Entity name</param>
    /// <returns>IMongoCollection T</returns>
    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        var database = _mongoConnection.GetDatabase();

        return name switch
        {
            not null=> database.GetCollection<T>(name),
            _ => GetCollection<T>(database)
        };
    }

    
}



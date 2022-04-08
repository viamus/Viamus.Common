namespace Viamus.Common.Library.Mongo.Entities;

using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using Viamus.Common.Library.Entities;
using Viamus.Common.Library.Mongo.Context;


/// <summary>
/// IEntityConfig abstraction, contains the must method for collection indexes creation
/// </summary>
public interface IEntityConfig
{
    public void CreateIndexes(MongoContext context);
}

/// <summary>
/// Abstract class wuth the logic of the collection indexes creation
/// </summary>
/// <typeparam name="T">Collection Entity</typeparam>

[ExcludeFromCodeCoverage]
public abstract class EntityConfig<T>: IEntityConfig where T : Entity
{
    protected IndexKeysDefinitionBuilder<T> Builder { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    protected EntityConfig()
    {
        Builder = Builders<T>.IndexKeys;
    }

    /// <summary>
    /// Abstract implementation of the IEntityConfig abstraction, this method is called for get collection indexes
    /// </summary>
    /// <returns>List<CreateIndexModel<T>></returns>
    protected abstract List<CreateIndexModel<T>> ConfigureIndexes();

    /// <summary>
    /// Implementation of the method used to create all entity mongo collection indexes
    /// </summary>
    /// <param name="context">MongoContext to get/create collection</param>
    public virtual void CreateIndexes(MongoContext context)
    {
        var collection = context.GetCollection<T>();

        var _existingIndexes = collection.Indexes.List().ToList().Select(c => c.GetValue("name").ToString());

        var indexes = ConfigureIndexes();

        if(collection is not null)
        {
            if (indexes is not null && indexes.Any())
            {
                if (indexes.Any())
                {
                    _= collection.Indexes.CreateMany(indexes);
                }
            }
        }
    }
}


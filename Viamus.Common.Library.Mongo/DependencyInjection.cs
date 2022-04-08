namespace Viamus.Common.Library.Mongo;

using Microsoft.Extensions.DependencyInjection;
using Viamus.Common.Library.Entities;
using Viamus.Common.Library.Mongo.Context;
using Viamus.Common.Library.Mongo.Entities;
using Viamus.Common.Library.Mongo.Repository;
using Viamus.Common.Library.Repository;


public static class ConfigureBindingsDependencyInjection
{
    /// <summary>
    /// Add mongo context dependency
    /// </summary>
    /// <param name="services">ServiceCollection</param>
    /// <param name="config">Action to configurate entityconfigs</param>
    public static void AddMongoContext
    (
        this IServiceCollection services, 
        Action<List<IEntityConfig>>? config = null
    )
    {
        services.AddSingleton<IMongoEntityConfig>(new MongoEntityConfig(config));

        services.AddSingleton<IMongoConnection, MongoConnection>();
        
        services.AddSingleton<IMongoContext, MongoContext>();
    }

    /// <summary>
    /// Add  repositories
    /// </summary>
    /// <typeparam name="TEntity">Entity collection</typeparam>
    /// <param name="service">ServiceCollection</param>
    public static void AddRepository<TEntity>(this IServiceCollection service) where TEntity: Entity
    {
        service.AddTransient<IRepository<TEntity>, DefaultRepository<TEntity>>();
    }
}
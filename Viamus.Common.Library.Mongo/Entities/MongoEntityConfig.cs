namespace Viamus.Common.Library.Mongo.Entities;

/// <summary>
/// Abstraction of IMongoEntityConfig, it has the methods for get all indexes of all application entities
/// </summary>
public interface IMongoEntityConfig
{
    public List<IEntityConfig> GetConfigs();
}

/// <summary>
/// Implementation of the IMongoEntityConfig
/// </summary>
public class MongoEntityConfig: IMongoEntityConfig
{
    private readonly List<IEntityConfig> _configs = new();

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="config">Action with all IEntityConfigs of your application</param>
    public MongoEntityConfig(Action<List<IEntityConfig>>? config)
    {
        config?.Invoke(_configs);
    }

    /// <summary>
    /// Return all indexes of your application
    /// </summary>
    /// <returns></returns>
    public List<IEntityConfig> GetConfigs()
    {
        return _configs;
    }
}
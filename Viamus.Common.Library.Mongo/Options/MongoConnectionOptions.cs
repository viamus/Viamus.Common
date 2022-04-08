namespace Viamus.Common.Library.Mongo.Options;

/// <summary>
/// MongoConnectionOptions configuration
/// </summary>
public class MongoConnectionOptions
{
    /// <summary>
    /// Mongo connectionstring
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Mongo schema
    /// </summary>
    public string Schema { get; set; } = string.Empty;
}
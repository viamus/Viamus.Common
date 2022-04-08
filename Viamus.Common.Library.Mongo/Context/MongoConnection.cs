namespace Viamus.Common.Library.Mongo.Context;

using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Viamus.Common.Library.Mongo.Options;
using Viamus.Common.Library.Mongo.Serializers;

/// <summary>
/// Abstraction MongoConnection
/// </summary>
public interface IMongoConnection
{
    IMongoDatabase GetDatabase();
}

/// <summary>
/// MongoConnection implementation. This contains the transport abstract to the mongodb server
/// </summary>
public class MongoConnection : IMongoConnection
{
    private static readonly object Lock = new();

    private bool _isRegistered = false;

    private readonly Dictionary<string, IMongoDatabase> Databases = new();

    private readonly string _connectionString;

    private readonly string _defaultDatabaseName;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="options">MongoConnectionOptions</param>
    public MongoConnection(IOptions<MongoConnectionOptions> options)
    {
        _connectionString = options.Value.ConnectionString!;
        _defaultDatabaseName = options.Value.Schema!;
    }

    /// <summary>
    /// Get a transport database comunication layer
    /// </summary>
    /// <returns>IMongoDatabase</returns>
    /// <exception cref="ArgumentNullException">If connectionstring or scheme is null in MongoConnectionOptions</exception>
    public IMongoDatabase GetDatabase()
    {
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new ArgumentNullException(_connectionString, "Mongo connection string is null.");
        }

        lock (Lock)
        {
            Databases.TryGetValue(_connectionString, out var database);

            if (database != null) return database;

            var urlBuilder = new MongoUrlBuilder(_connectionString);

            var databaseName = urlBuilder.DatabaseName;

            if (databaseName == null && string.IsNullOrEmpty(_defaultDatabaseName))
            {
                throw new ArgumentNullException(databaseName, "Mongo default database is null.");
            }

            databaseName = _defaultDatabaseName;

            var client = new MongoClient(urlBuilder.ToMongoUrl());
            database = client.GetDatabase(databaseName);

            Register();

            Databases[_connectionString] = database;

            return database;
        }
    }

    private void Register()
    {
        var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };

        ConventionRegistry.Register("IgnoreElements", conventionPack, type => true);

        if (!_isRegistered)
        {
            BsonSerializer.RegisterSerializer(typeof(DateTime), new UtcDateTimeSerializer());
            _isRegistered = true;
        }
    }
}

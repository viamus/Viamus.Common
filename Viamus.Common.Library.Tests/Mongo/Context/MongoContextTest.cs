namespace Viamus.Common.Library.Tests.Mongo.Context;

using MongoDB.Driver;
using NSubstitute;
using System.Collections.Generic;
using Viamus.Common.Library.Entities;
using Viamus.Common.Library.Mongo.Context;
using Viamus.Common.Library.Mongo.Entities;
using Xunit;

public class MongoContextTest
{
    public class EntityMock : Entity { }

    [Fact]
    public void GetCollection()
    {
        var collection = Substitute.For<IMongoCollection<EntityMock>>();

        var mongoDataBase = Substitute.For<IMongoDatabase>();
        mongoDataBase.GetCollection<EntityMock>(Arg.Any<string>(), null).Returns
        (
            collection
        );

        var connection = Substitute.For<IMongoConnection>();
        connection.GetDatabase().Returns(mongoDataBase);

        var mongoEntityConfig = Substitute.For<IMongoEntityConfig>();
        mongoEntityConfig.GetConfigs().Returns(new List<IEntityConfig>());

        var context = new MongoContext(connection, mongoEntityConfig);
 
        context.GetCollection<EntityMock>();
        context.GetCollection<EntityMock>("EntityMock");

        mongoDataBase.Received(2);
        connection.Received(2);
    }

    [Fact]
    public void CreateMongoContextWithNotEmptyEntityConfig()
    {
        var entityConfig = Substitute.For<IEntityConfig>();
        var connection = Substitute.For<IMongoConnection>();

        var mongoEntityConfig = Substitute.For<IMongoEntityConfig>();
        mongoEntityConfig.GetConfigs().Returns(new List<IEntityConfig> { entityConfig });

        _ = new MongoContext(connection, mongoEntityConfig);

        entityConfig.Received(1);
    }
}

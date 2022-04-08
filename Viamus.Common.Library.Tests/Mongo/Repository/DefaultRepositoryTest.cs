using MongoDB.Driver;
using NSubstitute;
using System;
using System.Collections.Generic;
using Viamus.Common.Library.Entities;
using Viamus.Common.Library.Mongo.Context;
using Viamus.Common.Library.Mongo.Entities;
using Viamus.Common.Library.Mongo.Repository;
using Viamus.Common.Library.Repository;
using Xunit;

namespace Viamus.Common.Library.Tests.Mongo.Repository
{
    public class DefaultRepositoryTest
    {
        public class EntityMock : Entity { }

        [Fact]
        public void DefaultOperations()
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

            IMongoContext context = new MongoContext(connection, mongoEntityConfig);

            IRepository<EntityMock> repository = new DefaultRepository<EntityMock>(context);

            repository.AddAsync(new EntityMock());
            repository.UpdateAsync((e)=> e.Id == Guid.NewGuid(), new EntityMock());
            _ = repository.GetAsync(Guid.NewGuid());
            _ = repository.GetAsync((e) => e.Id == Guid.NewGuid());


            collection.Received(4);
        }
    }
}

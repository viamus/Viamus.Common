using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using Viamus.Common.Library.Mongo.Context;
using Viamus.Common.Library.Mongo.Options;
using Xunit;

namespace Viamus.Common.Library.Tests.Mongo.Context
{
    public class MongoConnectionTest
    {
        [Fact]
        public void GetDatabaseThrowsExceptionWhenConnectionStringIsEmpty()
        {
            var options = Substitute.For<IOptions<MongoConnectionOptions>>();
            options.Value.Returns(new MongoConnectionOptions
            {
                ConnectionString = string.Empty
            });

            var connection = new MongoConnection(options);

            Assert.Throws<ArgumentNullException>(()=> connection.GetDatabase());
        }

        [Fact]
        public void GetDatabaseThrowsExceptionWhenSchemaIsEmpty()
        {
            var options = Substitute.For<IOptions<MongoConnectionOptions>>();
            options.Value.Returns(new MongoConnectionOptions
            {
                ConnectionString = "mongodb://user:password@localhost:27017",
                Schema = string.Empty
            });

            var connection = new MongoConnection(options);

            Assert.Throws<ArgumentNullException>(() => connection.GetDatabase());
        }

        [Fact]
        public void GetDatabaseReturnIMongoDatabaseWhenOptionsIsValid()
        {
            var options = Substitute.For<IOptions<MongoConnectionOptions>>();
            options.Value.Returns(new MongoConnectionOptions
            {
                ConnectionString = "mongodb://user:password@localhost:27017",
                Schema = "default"
            });

            var connection = new MongoConnection(options);

            connection.GetDatabase().Should().NotBe(null);
        }
    }
}

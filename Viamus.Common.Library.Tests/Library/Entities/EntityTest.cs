namespace Viamus.Common.Library.Tests.Library.Entities;

using FluentAssertions;
using System;
using System.Threading.Tasks;
using Viamus.Common.Library.Entities;
using Xunit;

public class EntityTest
{
    private class EntityMock : Entity
    {
        public EntityMock() : base() { }

        public EntityMock(Guid id) : base(id) { }
    }

    [Fact]
    public Task DefaultConstructor()
    {
        var entity = new EntityMock();

        entity.Id.Should().NotBe(Guid.Empty);
        entity.InsertedAt.Date.Should().Be(DateTime.Now.Date);
        entity.UpdatedAt.Date.Should().Be(DateTime.Now.Date);

        return Task.CompletedTask;
    }

    [Fact]
    public Task GuidConstructor()
    {
        var id = Guid.NewGuid();

        var entity = new EntityMock(id);

        entity.Id.Should().Be(id);
        entity.InsertedAt.Date.Should().Be(DateTime.Now.Date);
        entity.UpdatedAt.Date.Should().Be(DateTime.Now.Date);

        return Task.CompletedTask;
    }
}

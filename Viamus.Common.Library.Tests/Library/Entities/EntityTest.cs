namespace Viamus.Common.Library.Tests.Library.Entities;

using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

public class EntityTest
{
    [Fact]
    public Task DefaultConstructor()
    {
        var entity = EntitiesMock.GetEntity();

        entity.Id.Should().NotBe(Guid.Empty);
        entity.InsertedAt.Date.Should().Be(DateTime.Now.Date);
        entity.UpdatedAt.Date.Should().Be(DateTime.Now.Date);

        return Task.CompletedTask;
    }

    [Fact]
    public Task GuidConstructor()
    {
        var id = Guid.NewGuid();

        var entity = EntitiesMock.GetEntity(id);

        entity.Id.Should().Be(id);
        entity.InsertedAt.Date.Should().Be(DateTime.Now.Date);
        entity.UpdatedAt.Date.Should().Be(DateTime.Now.Date);

        return Task.CompletedTask;
    }
}

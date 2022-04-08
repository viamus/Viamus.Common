namespace Viamus.Common.Library.Tests.Library.Entities;

using System;
using Viamus.Common.Library.Entities;

internal class EntityMock: Entity
{
    public EntityMock() : base() { }

    public EntityMock(Guid id):base(id) {  }
}


internal static class EntitiesMock
{
    internal static Entity GetEntity() => new EntityMock();

    internal static Entity GetEntity(Guid id) => new EntityMock(id);
}

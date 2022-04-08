namespace Viamus.Common.Library.Mongo.Entities;

public abstract class ValueObject
{
    public DateTime InsertedAt { get; protected set; }

    protected ValueObject()
    {
        InsertedAt = DateTime.UtcNow;
    }

    protected ValueObject(Guid id)
    {
        InsertedAt = DateTime.UtcNow;
    }
}


namespace Viamus.Common.Library.Entities;

/// <summary>
/// Abstract class for Entities
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Entity identifier
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// UpdatedAt, if new must be equals to InsertAt
    /// </summary>
    public DateTime UpdatedAt { get; protected set; }

    /// <summary>
    /// InsertedAt, entity creation date
    /// </summary>
    public DateTime InsertedAt { get; protected set; }

    /// <summary>
    /// Default Constructor, must be used when creating a new entity
    /// </summary>
    protected Entity()
    {
        Id = Guid.NewGuid();
        InsertedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Guid Constructor, must be used when creating a new entity with a definied id
    /// </summary>
    protected Entity(Guid id)
    {
        Id = id;
        InsertedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}


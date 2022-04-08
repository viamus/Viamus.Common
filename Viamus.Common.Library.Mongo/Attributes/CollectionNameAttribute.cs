namespace Viamus.Common.Library.Mongo.Attributes;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Attribute for name a custom collection name on entities
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class CollectionNameAttribute : Attribute
{
    public string Name { get; init; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="name">Collection name</param>
    public CollectionNameAttribute(string name)
    {
        Name = name;
    }
}

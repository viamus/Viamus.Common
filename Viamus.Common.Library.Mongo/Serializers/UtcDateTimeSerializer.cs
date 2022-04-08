namespace Viamus.Common.Library.Mongo.Serializers;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Diagnostics.CodeAnalysis;


/// <summary>
/// Serializer to utc datetime to mongo server
/// </summary>
[ExcludeFromCodeCoverage]
public class UtcDateTimeSerializer : DateTimeSerializer
{
    public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = base.Deserialize(context, args);
        return DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        base.Serialize(context, args, value);
    }
}

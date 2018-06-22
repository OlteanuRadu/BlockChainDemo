using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace Blockchain.Data.Entities
{
    [DataContract]
    public abstract class Entity : IEntity
    {
        [DataMember]
        [BsonId]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
    }
}

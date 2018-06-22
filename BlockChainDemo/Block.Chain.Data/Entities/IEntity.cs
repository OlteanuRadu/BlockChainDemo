﻿using MongoDB.Bson.Serialization.Attributes;

namespace Blockchain.Data.Entities
{
    public interface IEntity<TKey>
    {
        /// <summary>
        ///     Gets or sets the Id of the Entity.
        /// </summary>
        /// <value>Id of the Entity.</value>
        [BsonId]
        TKey Id { get; set; }
    }

    /// <summary>
    ///     "Default" Entity interface.
    /// </summary>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public interface IEntity : IEntity<string>
    {

    }
}

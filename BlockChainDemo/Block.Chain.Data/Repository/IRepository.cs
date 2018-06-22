using Blockchain.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blockchain.Data.Repository
{
    public interface IRepository<T> : IRepository<T, string>
       where T : IEntity<string>
    {
    }

    public interface IRepository<T, in TKey> : IQueryable<T>
      where T : IEntity<TKey>
    {
        /// <summary>
        ///     Gets the Mongo collection (to perform advanced operations).
        /// </summary>
        /// <remarks>
        ///     One can argue that exposing this property (and with that, access to it's Database property for instance
        ///     (which is a "parent")) is not the responsibility of this class. Use of this property is highly discouraged;
        ///     for most purposes you can use the MongoRepositoryManager&lt;T&gt;
        /// </remarks>
        /// <value>The Mongo collection (to perform advanced operations).</value>
        IMongoCollection<T> Collection { get; }

        IQueryable<T> AsQueryable();

        T GetById(TKey id);

        T Add(T entity);

        void Add(IEnumerable<T> entities);

        T Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(TKey id);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> predicate);

        void DeleteAll();

        void Drop();

        long Count();

        bool Exists();

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}

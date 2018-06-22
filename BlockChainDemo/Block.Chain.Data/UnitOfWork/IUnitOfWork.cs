using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMongoClient Client { get; }

        IMongoDatabase DataBase { get; }

        IMongoDatabase CreateNewDatabase();
    }
}

using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using MongoDB.Driver;

namespace Blockchain.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        string connectionString =
  @"mongodb://shipowners:oQuGDEgCvKNDyANFwfpjhqzaEK2rOtDsLM1e7oCgOdegMRHgpmmF5d13TnrnGw5hDOemPRgxAmem9YCCIOlrtQ==@shipowners.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";

        private MongoUrl _mongoUrl = new MongoUrl(@"mongodb://shipownerss:81KXhFIpOsIWOogYWBU1kM97xQBSDtLhFxM51ZGFgV7ImaLtGYZQtk2nVT63SczZIA4RJxi7YH6D1pb9PxDT5g==@shipownerss.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");

        //public UnitOfWork(string connectionString) : this(new MongoUrl(connectionString)) { }
        public UnitOfWork()
        {
            var settings = MongoClientSettings.FromUrl(this._mongoUrl);
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            this.Client = new MongoClient(settings);
            this.DataBase = Client.GetDatabase("shipowners");
        }

        public IMongoClient Client { get; }

        public IMongoDatabase DataBase { get; }

        public IMongoDatabase CreateNewDatabase() => throw new NotImplementedException();
    }
}

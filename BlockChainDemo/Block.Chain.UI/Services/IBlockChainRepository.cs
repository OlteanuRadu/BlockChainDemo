using Blockchain.Data.Model;
using Blockchain.Data.Repository;
using Blockchain.Data.UnitOfWork;
using MongoDB.Bson.Serialization;

namespace BlockchainAPI.Services
{
    public interface IBlockChainRepository : IRepository<BlockChain>
    {
    }
    public class BlockChainRepository : BaseRepository<BlockChain>, IBlockChainRepository
    {
        public BlockChainRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            BsonClassMap.RegisterClassMap<CertificateViewModel>();
        }
    }


    public interface ICustomerRepository : IRepository<Customer>
    {
    }
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            //BsonClassMap.RegisterClassMap<CertificateViewModel>();
        }
    }

    public interface IShipRepository : IRepository<Ship>
    {
    }
    public class ShipRepository : BaseRepository<Ship>, IShipRepository
    {
        public ShipRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            //BsonClassMap.RegisterClassMap<CertificateViewModel>();
        }
    }
}

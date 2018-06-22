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
}

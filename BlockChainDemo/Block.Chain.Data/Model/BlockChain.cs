using Blockchain.Data.Entities;
using Blockchain.Data.Utils;
using System.Collections.Generic;

namespace Blockchain.Data.Model
{
    [CollectionName("BlockChain")]
    public class BlockChain : Entity
    {
        public IList<Block> Chain { get; set; }
    }
}

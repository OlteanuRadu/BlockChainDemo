using Blockchain.Data.Entities;
using Blockchain.Data.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Blockchain.Data.Model
{
    [DataContract]
    [CollectionName("Block")]
    public class Block : Entity
    {
        [DataMember]
        public string BlockType { get; set; }
        [DataMember]
        public DateTime Created { get; set; } = DateTime.Now;
        public BlockData Data { get; set; }
    }
}

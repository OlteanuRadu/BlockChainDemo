using Blockchain.Data.Entities;
using Blockchain.Data.Utils;
using System;
using System.Runtime.Serialization;

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
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }
        public BlockData Data { get; set; }
    }
}

using Blockchain.Data.Entities;
using Blockchain.Data.Utils;
using System;

namespace Blockchain.Data.Model
{
    [CollectionName("BlockData")]
    public class BlockData : Entity
    {
        public CertificateViewModel Content { get; set; }
    }

    public class CertificateViewModel : Entity
    {
        public string CustomerIdentifier { get; set; }
        public string VesselIdentifier { get; set; }
        public bool IsValid { get; set; } = false;
        public string CertificateIdentifier { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public byte[] File { get; set; }
    }
}

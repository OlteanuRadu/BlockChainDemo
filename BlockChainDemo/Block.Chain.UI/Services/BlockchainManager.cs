using Blockchain.Data.Model;
using BlockchainAPI.Controllers;
using BlockchainAPI.Events;
using BlockchainUI.Events;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainAPI.Services
{
    public class BlockChainManager<T> : IBlockChainManager where T: WpfCompositeEvent<object>
    {
        private IEventAggregator _eventAggregator { get; }
        private IBlockChainRepository _blockChainRepository { get; }

        private BlockChain currentBlockChain { get; set; }

        public BlockChainManager(IEventAggregator eventAggregator, IBlockChainRepository blockchainRepo)
        {
            this._eventAggregator = eventAggregator;
            this._blockChainRepository = blockchainRepo;
            this.currentBlockChain = this._blockChainRepository.FirstOrDefault() ?? new BlockChain { Chain = new List<Block>() };
            this._blockChainRepository.DeleteAll();
            this._blockChainRepository.Add(currentBlockChain);
            this.ProcessEvents();
        }

        public string CalculateMD5Hash(string input)
        {

            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }
            return sb.ToString();
        }


        public virtual void ProcessEvents()
        {
            this._eventAggregator
                .GetEvent<CertificateRequestControllerEvent<object>>()
                .RegisterHandlerInstance((_) =>
                {
                    this.currentBlockChain.Chain.Add(new Block
                    {
                        BlockType = nameof(CertificateRequestControllerEvent<CertificateViewModel>),
                        From = CalculateMD5Hash(nameof(BlockChainManager<CertificateRequestControllerEvent<object>>)),
                        To = CalculateMD5Hash(nameof(CertificateController))
                       // Data = new BlockData { Content = _ }
                    });
                    this._blockChainRepository.DeleteAll();
                    this._blockChainRepository.Add(this.currentBlockChain);
                });

            this._eventAggregator
                .GetEvent<UploadDocumentControllerEvent<CertificateViewModel>>()
                .RegisterHandlerInstance((_) =>
                {
                    this.currentBlockChain.Chain.Add(new Block
                    {
                        BlockType = nameof(UploadDocumentControllerEvent<CertificateViewModel>),
                        From = CalculateMD5Hash(nameof(BlockChainManager<CertificateRequestControllerEvent<object>>)),
                        To = CalculateMD5Hash(nameof(CertificateController)),
                        Data = new BlockData { Content = _ }
                    });
                    this._blockChainRepository.DeleteAll();
                    this._blockChainRepository.Add(this.currentBlockChain);
                });



            this._eventAggregator
                .GetEvent<ValidateCertificateControllerEvent<string>>()
                .RegisterHandlerInstance((_) =>
                {
                    this.currentBlockChain.Chain.Add(new Block
                    {
                        BlockType = nameof(ValidateCertificateControllerEvent<string>),
                        From = CalculateMD5Hash(nameof(BlockChainManager<ValidateCertificateControllerEvent<object>>)),
                        To = CalculateMD5Hash(_),
                       // Data = new BlockData { Content = _ }
                    });
                    this._blockChainRepository.DeleteAll();
                    this._blockChainRepository.Add(this.currentBlockChain);
                });

          

            this._eventAggregator
               .GetEvent< DownloadDocumentControllerEvent < IEnumerable < CertificateViewModel>>> ()
               .RegisterHandlerInstance((_) =>
               {
                   this.currentBlockChain.Chain.Add(new Block
                   {
                       BlockType = nameof(DownloadDocumentControllerEvent<CertificateViewModel>),
                       From= CalculateMD5Hash(nameof(DownloadDocumentControllerEvent<CertificateViewModel>)),
                       To=CalculateMD5Hash(_.FirstOrDefault().CustomerIdentifier)
                      // Data = new BlockData { Content = _ }
                   });
                   this._blockChainRepository.DeleteAll();
                   this._blockChainRepository.Add(this.currentBlockChain);
               });


        }
    }

    //public class BlockChainCertificateRequestHandler<CertificateRequestControllerEvent> : BlockChainManager<WpfCompositeEvent<object>>
    //{
    //    public BlockChainCertificateRequestHandler(IEventAggregator eventAgregator, IBlockChainRepository blockChainRepo) : base(eventAgregator, blockChainRepo) { }
    //    public override void ProcessEvents()
    //    {
    //        base.ProcessEvents();
    //    }
    //}



    public interface IBlockChainManager
    {

    }
}

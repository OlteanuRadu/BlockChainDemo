using Blockchain.Data.Model;
using BlockchainAPI.Controllers;
using BlockchainAPI.Events;
using BlockchainUI.Events;
using System.Collections.Generic;
using System.Linq;

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
        public virtual void ProcessEvents()
        {
            this._eventAggregator
                .GetEvent<CertificateRequestControllerEvent<object>>()
                .RegisterHandlerInstance((_) =>
                {
                    this.currentBlockChain.Chain.Add(new Block
                    {
                        BlockType = nameof(CertificateRequestControllerEvent<CertificateViewModel>),
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
                        Data = new BlockData { Content = _ }
                    });
                    this._blockChainRepository.DeleteAll();
                    this._blockChainRepository.Add(this.currentBlockChain);
                });

            this._eventAggregator
               .GetEvent<DownloadDocumentControllerEvent<object>>()
               .RegisterHandlerInstance((_) =>
               {
                   this.currentBlockChain.Chain.Add(new Block
                   {
                       BlockType = nameof(UploadDocumentControllerEvent<CertificateViewModel>),
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

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Data.Model;
using BlockchainAPI.Events;
using BlockchainAPI.Services;
using BlockchainUI.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainAPI.Controllers
{
    [Route("api/[controller]")]
    public class CertificateController : Controller
    {
        private IEventAggregator EventAggregator { get; }
        private IBlockChainRepository BlockChainRepo { get; }
        private IHostingEnvironment Env { get; }
        public CertificateController(IEventAggregator eventAggregator, IBlockChainRepository blockChainRepo, IHostingEnvironment env)
        {
            this.EventAggregator = eventAggregator;
            this.BlockChainRepo = blockChainRepo;
            this.Env = env;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<object> Get()
        {
            this
                .EventAggregator
                .GetEvent<CertificateRequestControllerEvent<object>>()
                .Publish(new object());
            var items = this.BlockChainRepo.FirstOrDefault().Chain.Select(_ => _.Data).OfType<BlockData>();
           
            return items.Select(_ => new
            {
                _.Content.CustomerIdentifier,
                _.Content.VesselIdentifier,
                _.Content.StartDate,
                _.Content.EndDate,
                _.Content.IsValid
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<object>> Get(string id)
        {
           //await Task.Run(() => this.BlockChainRepo.FirstOrDefault().Chain.Where(_ => _.Data.Content.CustomerIdentifier == @"cee2d424-f9de-4b7b-899f-69d963738fbd"));
           var res =  await Task.Run(() => this.BlockChainRepo.FirstOrDefault().Chain.Select(_ => _.Data).OfType<BlockData>().Select(_ => _.Content).Where(_ => _.CustomerIdentifier == id));
            return res.Select(_ => new
            {
                _.CustomerIdentifier,
                _.VesselIdentifier,
                _.StartDate,
                _.EndDate,
                _.IsValid
            });

            // return File(matchedFile.FirstOrDefault().Data.Content.File, "application/pdf", string.Empty);
        }

        [HttpGet("transaction")]
        public async Task<IEnumerable<Block>> GetAll()
        {
            var result = await Task.Run(() => this.BlockChainRepo.FirstOrDefault().Chain);
            return result;
        }

        [HttpGet("transaction/{blockType}")]
        public async Task<IEnumerable<Block>> GetByType(string blockType) =>
            await Task.Run(() => this.BlockChainRepo.FirstOrDefault().Chain.Where(_ => _.BlockType == blockType));

        [HttpGet("Download/{customerIdentifier}")]
        public async Task<IActionResult> Download(string customerIdentifier)
        {
            var matchedFile = await Task.Run(() => this.BlockChainRepo.FirstOrDefault().Chain.Select(_ => _.Data).OfType<BlockData>().Select(_ => _.Content).Where(_ => _.CustomerIdentifier == customerIdentifier));
            this.EventAggregator.GetEvent<DownloadDocumentControllerEvent<object>>().Publish(new object());

            return File(matchedFile.LastOrDefault().File, "application/pdf", string.Empty);
        }


        [HttpPost]
        [Route("validatecertificate")]
        //api/certificate/ValidateCertificate
        public async Task<bool> ValidateCertificate(ValidateCertificateViewModel vm) =>
            await Task.Run(() => true);

        [HttpPost]
        //[Produces("application/json")]
        //api/certificate/ValidateCertificate
        [Route("upload")]
        public async Task<IActionResult> Upload(FileUploadViewModel vm)
        {
            vm.CustomerIdentifier = @"cee2d424-f9de-4b7b-899f-69d963738fbd";

            using (var ms = new MemoryStream())
            {
                await vm.File.CopyToAsync(ms);
                this.EventAggregator.GetEvent<UploadDocumentControllerEvent<CertificateViewModel>>().Publish(new CertificateViewModel
                {
                    CertificateIdentifier = Guid.NewGuid().ToString(),
                    CustomerIdentifier = vm.CustomerIdentifier,
                    VesselIdentifier = Guid.NewGuid().ToString(),
                    File = ms.ToArray()
                });
            }

            return await Task.Run(() => Ok());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class ValidateCertificateViewModel
    {
        public string VesselIdentifier { get; set; }
        public string CertificateIdentifier { get; set; }
        public DateTime CurrentTime { get; set; }
    }

    public class FileUploadViewModel
    {
        public string CustomerIdentifier { get; set; }
        public IFormFile File { get; set; }
    }

    public class FileDownloadViewModel
    {
        public string CustomerIdentifier { get; set; }
        public string VesselIdentifier { get; set; }
    }

}

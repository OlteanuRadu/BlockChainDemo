
using Blockchain.Data.Model;
using BlockchainAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainUI.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        public TokenAuthController(ICustomerRepository customerRepo) =>
            this._customerRepo = customerRepo;

        [HttpPut("Login")]
        public IActionResult Login([FromBody]Customer customer)
        {
            return null;
        }
    }
}
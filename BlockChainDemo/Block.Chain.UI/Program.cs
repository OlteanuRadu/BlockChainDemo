using System;
using BlockchainAPI.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication7
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var host =   CreateWebHostBuilder(args);
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var serviceContext = services.GetRequiredService<IBlockChainManager>();

                    var customerRepo = services.GetRequiredService<ICustomerRepository>();
                    //customerRepo.Add(new Blockchain.Data.Model.Customer
                    //{
                    //    FirstName = "Tyrion",
                    //    LastName = "Lannister"
                    //});

                    //customerRepo.Add(new Blockchain.Data.Model.Customer
                    //{
                    //    FirstName = "Jon",
                    //    LastName = "Snow"
                    //});

                    //customerRepo.Add(new Blockchain.Data.Model.Customer
                    //{
                    //    FirstName = "Daenerys",
                    //    LastName = "Targaryen"
                    //});

                    //customerRepo.Add(new Blockchain.Data.Model.Customer
                    //{
                    //    FirstName = "Radu",
                    //    LastName = "Olteanu"
                    //});

                    //var shipRepo = services.GetRequiredService<IShipRepository>();
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Atropos"
                    //});

                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Harpy  "
                    //});
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Lydia"
                    //});
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Phoebe"
                    //});
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Themis"
                    //});
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Witch of Endor"
                    //});
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Justinian"
                    //});
                    //shipRepo.Add(new Blockchain.Data.Model.Ship
                    //{
                    //    ShipName = "Clorinda"
                    //});

                }
                catch (Exception ex)
                {

                }
            }
            host.Run();
            Console.ReadKey();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}

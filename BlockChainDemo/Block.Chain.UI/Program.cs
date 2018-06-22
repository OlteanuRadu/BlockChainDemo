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

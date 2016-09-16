using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("hosting.json", optional: true)
                    .Build();

                var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(builder)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

                host.Run();

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

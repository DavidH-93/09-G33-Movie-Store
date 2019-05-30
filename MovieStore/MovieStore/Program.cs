using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieStore.Data;

namespace MovieStore {
    public class Program {
        public static void Main(string[] args) {
            IWebHost host = BuildWebHost(args);

            using (IServiceScope scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                try {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var context = services.GetRequiredService<MovieStoreDbContext>();

                    Seed.CreateRoles(serviceProvider, configuration).Wait();
                    Seed.SeedCustomers(serviceProvider, configuration, context).Wait();
                }
                catch (Exception exception) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An error occurred while creating roles");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

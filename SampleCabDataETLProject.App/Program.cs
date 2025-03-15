using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleCabDataETLProject.App.HostBuilders;
using SampleCabDataETLProject.DAL.DataProcessors;
using SampleCabDataETLProject.DAL.Persistence;
using SampleCabDataETLProject.DAL.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.App
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = AppHostBuilder.CreateHostBuilder.Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<AppDbContext>();
                CabDataProcessor.CleanUpData();
                var seeder = new InitialDataSeeding(dbContext);
                await seeder.SeedInitialData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed: {ex.Message}");
            }
        }
    }
}

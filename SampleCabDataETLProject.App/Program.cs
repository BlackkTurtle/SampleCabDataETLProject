using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleCabDataETLProject.App.HostBuilders;
using SampleCabDataETLProject.BLL.Services;
using SampleCabDataETLProject.BLL.Services.Contracts;
using SampleCabDataETLProject.DAL.DataProcessors;
using SampleCabDataETLProject.DAL.Persistence;
using SampleCabDataETLProject.DAL.Seeding;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

            while (true)
            {
                Console.ReadLine();
                Console.WriteLine("Choose the option:\n" +
                    "Find out which `PULocationId` (Pick-up location ID) has the highest tip_amount on average. - 1\n" +
                    "Find the top 100 longest fares in terms of `trip_distance`. - 2\n" +
                    "Find the top 100 longest fares in terms of time spent traveling. - 3\n" +
                    "Search, where part of the conditions is `PULocationId`. - 4\n" +
                    "Close the application -  close");

                var service = services.GetRequiredService<ISampleCabDataService>();

                var input = Console.ReadLine();
                if (input == "1")
                {
                    var result = await service.GetPULocationIDWithTheHighestAverageTip();
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine(result);
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                }
                else if (input == "2")
                {
                    var result = await service.GetTop100LongestFaresBasedOnTripDistance();
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine(result);
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                }
                else if (input == "3")
                {
                    var result = await service.GetTop100LongestFaresBasedOnTimeTravelled();
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine(result);
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                }
                else if (input == "4")
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.Write("Enter the PULocationID: ");
                    input = Console.ReadLine();
                    try
                    {
                        int PULocationId = int.Parse(input);
                        var result = await service.GetByPULocationId(PULocationId);
                        Console.WriteLine("---------------------------------------------------------------------------------------");
                        Console.WriteLine(result);
                        Console.WriteLine("---------------------------------------------------------------------------------------");
                    }
                    catch
                    {
                        Console.WriteLine("---------------------------------------------------------------------------------------");
                        Console.WriteLine("The provided PULocationID was not valid!");
                        Console.WriteLine("---------------------------------------------------------------------------------------");
                    }
                }
                else if (input == "close") { break; }
                else
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine("The provided option was not valid!");
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                };
            }
        }
    }
}

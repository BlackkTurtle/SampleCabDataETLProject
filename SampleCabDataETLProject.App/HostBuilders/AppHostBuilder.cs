using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleCabDataETLProject.DAL.Infrastructure;
using SampleCabDataETLProject.DAL.Infrastructure.Interfaces;
using SampleCabDataETLProject.DAL.Persistence;
using SampleCabDataETLProject.DAL.Repositories;
using SampleCabDataETLProject.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.App.HostBuilders
{
    public static class AppHostBuilder
    {
        public static IHostBuilder CreateHostBuilder =>
            Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Add DbContext
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString));

                // Add repositories
                services.AddTransient<ISampleCabDataRepository, SampleCabDataRepository>();
                // Other
                services.AddTransient<IUnitOfWork, UnitOfWork>();
            });
    }
}

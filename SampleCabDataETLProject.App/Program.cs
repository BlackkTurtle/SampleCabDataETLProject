using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleCabDataETLProject.App.HostBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            using IHost host = AppHostBuilder.CreateHostBuilder.Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
        }
    }
}

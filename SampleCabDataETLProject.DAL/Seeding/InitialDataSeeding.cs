using SampleCabDataETLProject.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Seeding
{
    public static class InitialDataSeeding
    {
        public static Task SeedInitialData(AppDbContext context)
        {
            return Task.CompletedTask;
        }
    }
}

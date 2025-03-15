using Microsoft.SqlServer.Server;
using SampleCabDataETLProject.DAL.DataProcessors;
using SampleCabDataETLProject.DAL.Entities;
using SampleCabDataETLProject.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Seeding
{
    public class InitialDataSeeding
    {
        private AppDbContext context;

        public InitialDataSeeding(AppDbContext context)
        {
            this.context = context;
        }

        public async Task SeedInitialData()
        {
            if (!(this.context.CabDataEntities.Count() > 0))
            {
                string uniqueFilePath = "../../../../SampleCabDataETLProject.DAL/Seeding/unique.csv";

                string dateFormat = "MM/dd/yyyy hh:mm:ss tt";

                List<string[]> rows = CabDataProcessor.ReadCsv(uniqueFilePath);

                var entities = new List<SampleCabDataEntity>();
                foreach (string[] row in rows) 
                {
                    DateTime.TryParseExact(row[1], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime field1);
                    DateTime.TryParseExact(row[2], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime field2);
                    double.TryParse(row[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double field4);
                    double.TryParse(row[10], NumberStyles.Any, CultureInfo.InvariantCulture, out double field8);
                    double.TryParse(row[13], NumberStyles.Any, CultureInfo.InvariantCulture, out double field9);

                    var entity = new SampleCabDataEntity
                    {
                        tpep_pickup_datetime = field1.AddHours(5),
                        tpep_dropoff_datetime = field2.AddHours(5),
                        passenger_count = int.Parse(row[3]),
                        trip_distance = field4,
                        store_and_fwd_flag = row[6],
                        PULocationID = int.Parse(row[7]),
                        DOLocationID = int.Parse(row[8]),
                        fare_amount = field8,
                        tip_amount = field9,
                    };
                    entities.Add(entity);
                }
                await context.CabDataEntities.AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}

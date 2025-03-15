using Microsoft.EntityFrameworkCore;
using SampleCabDataETLProject.DAL.DTOs.SampleCabDataDTOs;
using SampleCabDataETLProject.DAL.Entities;
using SampleCabDataETLProject.DAL.Infrastructure;
using SampleCabDataETLProject.DAL.Persistence;
using SampleCabDataETLProject.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Repositories
{
    public class SampleCabDataRepository : GenericRepository<SampleCabDataEntity>, ISampleCabDataRepository
    {
        private readonly AppDbContext context;
        public SampleCabDataRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SampleCabDataEntity>> GetByPULocationId(int id)
        {
            return await context.CabDataEntities
                .Where(x => x.PULocationID == id)
                .ToListAsync();
        }

        public async Task<PULocationWithAverageTipDTO> GetPULocationIDWithTheHighestAverageTip()
        {
            var result = await context.CabDataEntities
                .GroupBy(c => c.PULocationID)
                .Select(g => new
                {
                    PULocationID = g.Key,
                    AverageTip = g.Average(c => c.tip_amount)
                })
                .OrderByDescending(x => x.AverageTip)
                .FirstOrDefaultAsync();

            return new PULocationWithAverageTipDTO { PULocationId = result.PULocationID, AverageTip = result.AverageTip } ?? null;
        }

        public async Task<IEnumerable<SampleCabDataWithTimeSpentDTO>> GetTop100LongestFaresBasedOnTimeTravelled()
        {
            var results = await context.CabDataEntities
                .Select(c => new
                {
                    c.Id,
                    c.tpep_pickup_datetime,
                    c.tpep_dropoff_datetime,
                    c.trip_distance,
                    c.fare_amount,
                    TimeSpent = EF.Functions.DateDiffSecond(c.tpep_pickup_datetime, c.tpep_dropoff_datetime)
                })
                .OrderByDescending(c => c.TimeSpent)
                .Take(100)
                .ToListAsync();

            List<SampleCabDataWithTimeSpentDTO> result = new();

            foreach (var fare in results)
            {
                var dto = new SampleCabDataWithTimeSpentDTO
                {
                    Id = fare.Id,
                    trip_distance = fare.trip_distance,
                    fare_amount = fare.fare_amount,
                    TimeSpent = TimeSpan.FromSeconds(fare.TimeSpent)
                };

                result.Add(dto);
            }
            return result;
        }

        public async Task<IEnumerable<SampleCabDataEntity>> GetTop100LongestFaresBasedOnTripDistance()
        {
            return await context.CabDataEntities
                .OrderByDescending(c => c.trip_distance)
                .Take(100) 
                .ToListAsync();
        }
    }
}

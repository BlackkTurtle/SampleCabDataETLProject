using Microsoft.EntityFrameworkCore;
using SampleCabDataETLProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<SampleCabDataEntity> CabDataEntities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SampleCabDataEntity>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.tpep_pickup_datetime);
                entity.Property(e => e.tpep_dropoff_datetime);
                entity.Property(e => e.passenger_count);
                entity.Property(e => e.trip_distance);
                entity.Property(e => e.store_and_fwd_flag).HasMaxLength(3).IsRequired();
                entity.Property(e => e.PULocationID);
                entity.Property(e => e.DOLocationID);
                entity.Property(e => e.fare_amount);
                entity.Property(e => e.tip_amount);
            });
        }
    }
}

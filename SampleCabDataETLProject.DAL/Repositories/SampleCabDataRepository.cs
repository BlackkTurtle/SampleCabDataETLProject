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
    }
}

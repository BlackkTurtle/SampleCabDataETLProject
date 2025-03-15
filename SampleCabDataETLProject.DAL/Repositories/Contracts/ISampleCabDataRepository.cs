using SampleCabDataETLProject.DAL.Entities;
using SampleCabDataETLProject.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Repositories.Contracts
{
    public interface ISampleCabDataRepository : IGenericRepository<SampleCabDataEntity>
    {
    }
}

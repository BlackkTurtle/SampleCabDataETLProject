using SampleCabDataETLProject.DAL.DTOs.SampleCabDataDTOs;
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
        Task<PULocationWithAverageTipDTO> GetPULocationIDWithTheHighestAverageTip();
        Task<IEnumerable<SampleCabDataEntity>> GetTop100LongestFaresBasedOnTripDistance();
        Task<IEnumerable<SampleCabDataWithTimeSpentDTO>> GetTop100LongestFaresBasedOnTimeTravelled();
        Task<IEnumerable<SampleCabDataEntity>> GetByPULocationId(int id);
    }
}

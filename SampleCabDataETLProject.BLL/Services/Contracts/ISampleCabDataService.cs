using SampleCabDataETLProject.DAL.DTOs.SampleCabDataDTOs;
using SampleCabDataETLProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.BLL.Services.Contracts
{
    public interface ISampleCabDataService
    {
        Task<string> GetPULocationIDWithTheHighestAverageTip();
        Task<string> GetTop100LongestFaresBasedOnTripDistance();
        Task<string> GetTop100LongestFaresBasedOnTimeTravelled();
        Task<string> GetByPULocationId(int id);
    }
}

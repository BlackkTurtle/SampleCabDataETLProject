using SampleCabDataETLProject.BLL.Services.Contracts;
using SampleCabDataETLProject.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.BLL.Services
{
    public class SampleCabDataService : ISampleCabDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SampleCabDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetByPULocationId(int id)
        {
            try
            {
                var data = await _unitOfWork.SampleCabDataRepository.GetByPULocationId(id);

                if (data.Count() == 0)
                {
                    return $"No fares with PULocationID: '{id}' found!";
                }

                StringBuilder result = new();

                foreach (var item in data)
                {
                    result.Append($"ID: {item.Id}, " +
                      $"Pickup: {item.tpep_pickup_datetime}, " +
                      $"Dropoff: {item.tpep_dropoff_datetime}, " +
                      $"Passengers: {item.passenger_count}, " +
                      $"Distance: {item.trip_distance}, " +
                      $"Store & Fwd: {item.store_and_fwd_flag}, " +
                      $"PULocationID: {item.PULocationID}, " +
                      $"DOLocationID: {item.DOLocationID}, " +
                      $"Fare: {item.fare_amount}, " +
                      $"Tip: {item.tip_amount}\n");
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Something went wrong while executing the DB query: {ex.Message}";
            }
        }

        public async Task<string> GetPULocationIDWithTheHighestAverageTip()
        {
            try
            {
                var dataDTO = await _unitOfWork.SampleCabDataRepository.GetPULocationIDWithTheHighestAverageTip();

                if (dataDTO == null) 
                {
                    return "No PULocationIDs found!";
                }

                return ($"PULocationID with highest average tip: {dataDTO.PULocationId}, Average Tip: {dataDTO.AverageTip}");
            }
            catch (Exception ex) {
                return $"Something went wrong while executing the DB query: {ex.Message}";
            }
        }

        public async Task<string> GetTop100LongestFaresBasedOnTimeTravelled()
        {
            try
            {
                var data = await _unitOfWork.SampleCabDataRepository.GetTop100LongestFaresBasedOnTimeTravelled();

                if (data.Count() == 0)
                {
                    return "No results found!";
                }

                StringBuilder result = new();

                foreach (var item in data)
                {
                    result.Append($"ID: {item.Id}, Time Spent: {item.TimeSpent}, Trip Distance: {item.trip_distance}, Fare Amount: {item.fare_amount}\n");
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Something went wrong while executing the DB query: {ex.Message}";
            }
        }

        public async Task<string> GetTop100LongestFaresBasedOnTripDistance()
        {
            try
            {
                var data = await _unitOfWork.SampleCabDataRepository.GetTop100LongestFaresBasedOnTripDistance();

                if (data.Count() == 0)
                {
                    return "No results found!";
                }

                StringBuilder result = new();

                foreach (var item in data) 
                {
                    result.Append($"ID: {item.Id}, Trip Distance: {item.trip_distance}, Fare Amount: {item.fare_amount}\n");
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Something went wrong while executing the DB query: {ex.Message}";
            }
        }
    }
}

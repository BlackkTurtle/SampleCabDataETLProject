using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.DTOs.SampleCabDataDTOs
{
    public class SampleCabDataWithTimeSpentDTO
    {
        public int Id { get; set; }
        public double trip_distance { get; set; }
        public double fare_amount { get; set; }
        public TimeSpan TimeSpent {  get; set; }
    }
}

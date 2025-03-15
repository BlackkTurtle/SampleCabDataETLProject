using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.DTOs.SampleCabDataDTOs
{
    public class PULocationWithAverageTipDTO
    {
        public int PULocationId { get; set; }
        public double AverageTip {  get; set; }
    }
}

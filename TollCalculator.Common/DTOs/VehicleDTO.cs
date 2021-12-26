using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Common.Enums;

namespace TollCalculator.Common.DTOs
{
    public class VehicleDTO
    {
        public long VehicleId { get; set; }
        public string NumberPlate { get; set; }
        public int ? VehicleType { get; set; }
        public bool IsTollApplicable
        {
            get; set;
        }
    }
}

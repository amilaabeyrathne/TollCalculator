using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Common.DTOs;
using TollCalculator.Common.Enums;
using TollCalculator.Services.Interfaces;

namespace TollCalculator.Services
{
    public class VehicleService : IVehicleService
    {
        public bool IsTollNotApplicable(int? vehicleType)
        {
            if (vehicleType == null) return false;
            bool isApplicable  = Enum.IsDefined(typeof(TollFreeVehicleTypesEnum), (TollFreeVehicleTypesEnum)vehicleType);

            return isApplicable;
        }
    }
}

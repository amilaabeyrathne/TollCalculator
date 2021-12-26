using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculator.Common.Enums
{
    /// <summary>
    /// Vehicles which do not have Toll fees
    /// </summary>
    public enum TollFreeVehicleTypesEnum
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}

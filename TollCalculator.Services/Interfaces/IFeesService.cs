using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Common.DTOs;

namespace TollCalculator.Services.Interfaces
{
    public interface IFeesService
    {
        decimal GetTotalFee(List<DateTime> recordedTimes, List<FeesRangeDTO> feesRangeDTOList);
       
    }
}

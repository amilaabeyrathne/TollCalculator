using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Common.DTOs;
using TollCalculator.Services.Interfaces;

namespace TollCalculator.Services
{
    public class FeesService : IFeesService
    {
        /// <summary>
        /// Time is needed in 24 hrs
        /// </summary>
        /// <param name="recordedTimes"></param>
        /// <param name="feesRangeDTOList"></param>
        /// <returns></returns>
        public decimal GetTotalFee(List<DateTime> recordedTimes, List<FeesRangeDTO> feesRangeDTOList)
        {
            decimal total = 0.0M;

            if (recordedTimes == null || feesRangeDTOList == null) return total;

            DateTime intervalStart = recordedTimes[0];
            double hrs = intervalStart.Hour;
            double minutes = (double)intervalStart.Minute / 100;
            double time = hrs + minutes;
            total = GetFee(hrs + minutes, feesRangeDTOList);

            int count = 0;

            foreach (var recordedTime in recordedTimes)
            {
                if ((recordedTime - intervalStart).TotalMinutes <= 60.0) // charged only once for an hour
                {
                    count += 1;
                    continue;
                }
                
                intervalStart = recordedTimes[count];
                count += 1;

                hrs = recordedTime.Hour;
                minutes = (double)recordedTime.Minute / 100;
                time = hrs + minutes;

                total += GetFee(hrs + minutes, feesRangeDTOList);
            }

            return total;
        }

        /// <summary>
        /// get the fee for the time
        /// </summary>
        /// <param name="time">in 24 hrs</param>
        /// <param name="feesRangeDTOList"></param>
        /// <returns></returns>
        private decimal GetFee(double time, List<FeesRangeDTO> feesRangeDTOList)
        {
            decimal fee = 0.0M;

            var range = feesRangeDTOList.Where(x => x.StartTime <= time && x.EndTime >= time).FirstOrDefault();

            if (range != null) return range.Fee;

            return fee;

        }
    }
}

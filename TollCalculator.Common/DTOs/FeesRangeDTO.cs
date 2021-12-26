using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculator.Common.DTOs
{
    public class FeesRangeDTO
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public decimal Fee { get; set; }
        public double StartHour { get; set; }
        public double EndHour { get; set; }

    }
}

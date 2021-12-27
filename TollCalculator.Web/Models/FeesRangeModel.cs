using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TollCalculator.Web.Models
{
    public class FeesRangeModel
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public decimal Fee { get; set; }
        public double StartHour { get; set; }
        public double EndHour { get; set; }
    }
}
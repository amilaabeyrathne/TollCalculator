using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TollCalculator.Web.Models
{
    public class FeeModel
    {
        public long VehicleId { get; set; }
        public int VehicleType { get; set; }
        public string Date { get; set; }
        public List<FeesRangeModel> FeesRangeList { get; set; }
        public List<TimeIntervalModel> TimeIntervalList { get; set; }
    }
}                           
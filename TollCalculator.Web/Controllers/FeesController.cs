using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TollCalculator.Common.DTOs;
using TollCalculator.Services;
using TollCalculator.Services.Interfaces;

namespace TollCalculator.Web.Controllers
{
    public class FeesController : ApiController
    {
        private readonly IFeesService feesService;
        public FeesController()
        {
            this.feesService = new FeesService();
        }

        public string Get()
        {
            var  feesRangeLsit = new List<FeesRangeDTO>()
            {
              new FeesRangeDTO(){ StartTime=6.0 ,EndTime=6.29, Fee =9 },
              new FeesRangeDTO(){ StartTime=6.30 ,EndTime=6.59, Fee =16 },
              new FeesRangeDTO(){ StartTime=7.0 ,EndTime=7.59, Fee =22 },
              new FeesRangeDTO(){ StartTime=8.0 ,EndTime=8.29, Fee =16 },
              new FeesRangeDTO(){ StartTime=8.30 ,EndTime=14.59, Fee =9 },
              new FeesRangeDTO(){ StartTime=15.0 ,EndTime=15.29, Fee =16 },
              new FeesRangeDTO(){ StartTime=15.30 ,EndTime=16.59, Fee =22 },
              new FeesRangeDTO(){ StartTime=17.0 ,EndTime=17.59, Fee =16 },
              new FeesRangeDTO(){ StartTime=18.0 ,EndTime=18.29, Fee =9 },
              new FeesRangeDTO(){ StartTime=18.30 ,EndTime=23.59, Fee =0 },
              new FeesRangeDTO(){ StartTime=0.0 ,EndTime=5.59, Fee =0 },
            };

            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");

            timeIntervals.Add(date + new TimeSpan(6, 55, 0));//16 =>added
            timeIntervals.Add(date + new TimeSpan(7, 0, 0));//22 => not
            timeIntervals.Add(date + new TimeSpan(7, 25, 0));//22 => not
            timeIntervals.Add(date + new TimeSpan(7, 57, 0));//22 => added
            timeIntervals.Add(date + new TimeSpan(8, 0, 0));//16 => not
            timeIntervals.Add(date + new TimeSpan(8, 35, 0));//9 => not

            var total = this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            return total.ToString();
        }
       
    }
}
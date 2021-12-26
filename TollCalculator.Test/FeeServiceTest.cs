using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TollCalculator.Common.DTOs;
using TollCalculator.Services;
using TollCalculator.Services.Interfaces;

namespace TollCalculator.Test
{
    [TestClass]
    public class FeeServiceTest
    {
        private IFeesService feesService;
        private List<FeesRangeDTO> feesRangeLsit;

        [TestInitialize]
        public void Init()
        {
            this.feesService = new FeesService();
            feesRangeLsit = new List<FeesRangeDTO>()
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
        }

        /// <summary>
        /// added once in hour
        /// </summary>
        [TestMethod]
        public void FeesService_GetTotalFee_OnceInAnHour_Returens_Total()
        {
            var total = 38.0M;

            List<DateTime> timeIntervals = new List<DateTime>();

            timeIntervals.Add(DateTime.ParseExact("06:55", "H:m", null));//16 =>added
            timeIntervals.Add(DateTime.ParseExact("07:00", "H:m", null));//22 => not
            timeIntervals.Add(DateTime.ParseExact("07:25", "H:m", null));//22 => not
            timeIntervals.Add(DateTime.ParseExact("07:57", "H:m", null));//22 => added
            timeIntervals.Add(DateTime.ParseExact("08:00", "H:m", null));//16 => not
            timeIntervals.Add(DateTime.ParseExact("08:35", "H:m", null));//9 => not

            var resut = this.feesService.GetTotalFee(timeIntervals, feesRangeLsit);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// added once in hour after noon
        /// </summary>
        [TestMethod]
        public void FeesService_GetTotalFee_Afternoon_Returens_Total()
        {
            var total = 27.0M;

            List<DateTime> timeIntervals = new List<DateTime>();

            timeIntervals.Add(DateTime.ParseExact("13:30", "H:m", null));//9 =>added
            timeIntervals.Add(DateTime.ParseExact("14:29", "H:m", null));//9 => not
            timeIntervals.Add(DateTime.ParseExact("14:31", "H:m", null));//9 => added
            timeIntervals.Add(DateTime.ParseExact("18:20", "H:m", null));//9 => added
            timeIntervals.Add(DateTime.ParseExact("18:35", "H:m", null));//0 => not
            timeIntervals.Add(DateTime.ParseExact("23:26", "H:m", null));//0 => added

            var resut = this.feesService.GetTotalFee(timeIntervals, feesRangeLsit);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// All in an one hour
        /// </summary>
        [TestMethod]
        public void FeesService_GetTotalFee_OneHour_Returens_Total()
        {
            var total = 16.0M;

            List<DateTime> timeIntervals = new List<DateTime>();

            timeIntervals.Add(DateTime.ParseExact("15:00", "H:m", null));//16 =>added
            timeIntervals.Add(DateTime.ParseExact("15:12", "H:m", null));//16=> not
            timeIntervals.Add(DateTime.ParseExact("15:31", "H:m", null));//22 => not
            timeIntervals.Add(DateTime.ParseExact("15:45", "H:m", null));//22 => not
            timeIntervals.Add(DateTime.ParseExact("15:55", "H:m", null));//22=> not
            timeIntervals.Add(DateTime.ParseExact("16:00", "H:m", null));//22 => not

            var resut = this.feesService.GetTotalFee(timeIntervals, feesRangeLsit);

            Assert.AreEqual(total, resut);
        }
    }
}

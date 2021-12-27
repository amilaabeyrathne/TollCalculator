using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task FeesService_GetTotalFee_OnceInAnHour_Returens_Total()
        {
            var total = 38.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");

            timeIntervals.Add(date+ new TimeSpan(6, 55, 0));//16 =>added
            timeIntervals.Add(date + new TimeSpan(7, 0, 0));//22 => not
            timeIntervals.Add(date + new TimeSpan(7, 25, 0));//22 => not
            timeIntervals.Add(date + new TimeSpan(7, 57, 0));//22 => added
            timeIntervals.Add(date + new TimeSpan(8, 0, 0));//16 => not
            timeIntervals.Add(date + new TimeSpan(8, 35, 0));//9 => not

            var resut =  await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// added once in hour after noon
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_Afternoon_Returens_Total()
        {
            var total = 27.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");

            timeIntervals.Add(date + new TimeSpan(13, 30, 0));//9 =>added
            timeIntervals.Add(date + new TimeSpan(14, 29, 0));//9 =>not
            timeIntervals.Add(date + new TimeSpan(14, 31, 0));//9 => added
            timeIntervals.Add(date + new TimeSpan(18, 20, 0));//22 => added
            timeIntervals.Add(date + new TimeSpan(18, 35, 0));//16 => not
            timeIntervals.Add(date + new TimeSpan(23, 26, 0));//16 => added

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// All in an one hour
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_OneHour_Returens_Total()
        {
            var total = 16.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");

            timeIntervals.Add(date + new TimeSpan(15, 00, 0));//9 =>added
            timeIntervals.Add(date + new TimeSpan(15, 12, 0));//9 =>not
            timeIntervals.Add(date + new TimeSpan(15, 31, 0));//9 => not
            timeIntervals.Add(date + new TimeSpan(15, 45, 0));//22 => not
            timeIntervals.Add(date + new TimeSpan(15, 55, 0));//16 => not
            timeIntervals.Add(date + new TimeSpan(16, 0, 0));//16 => added

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// Test holidays
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_HoliDay_Returens_Total() 
        {
            var total = 0.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 24, 2021");
            timeIntervals.Add(date + new TimeSpan(15, 00, 0));

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// Test holidays - not a holiday
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_NotHoliDay_Returens_Total()
        {
            var total = 0.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");
            timeIntervals.Add(date + new TimeSpan(15, 00, 0));

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreNotEqual(total, resut);
        }

        /// <summary>
        /// Test Week end - not a holiday
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_WeekEnd_Returens_Total()
        {
            var total = 0.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 4, 2021");
            timeIntervals.Add(date + new TimeSpan(15, 00, 0));

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// Exceptions
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_Error_Returens_Total()
        {
            var appender = new log4net.Appender.MemoryAppender();
            BasicConfigurator.Configure(appender);

            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 4, 2021");

            var resut = await 
                this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            var logEntries = appender.GetEvents();
            Assert.IsTrue(logEntries.Any());
        }

        /// <summary>
        /// Total equals possible max total
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_Equals_PossibleMaxValue_Returens_Total()
        {
            var total = 60.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");

            timeIntervals.Add(date + new TimeSpan(7, 05, 0));//16 =>added
            timeIntervals.Add(date + new TimeSpan(8, 25, 0));//22 => added
            timeIntervals.Add(date + new TimeSpan(15, 45, 0));//22 => added

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }

        /// <summary>
        /// Total greater possible max total
        /// </summary>
        [TestMethod]
        public async Task FeesService_GetTotalFee_GreaterThan_PossibleMaxValue_Returens_Total()
        {
            var total = 60.0M;
            var vehicleType = 20;// vehicle type enum
            List<DateTime> timeIntervals = new List<DateTime>();

            var date = DateTime.Parse("Dec 27, 2021");

            timeIntervals.Add(date + new TimeSpan(7, 05, 0));//16 =>added
            timeIntervals.Add(date + new TimeSpan(8, 25, 0));//22 => added
            timeIntervals.Add(date + new TimeSpan(15, 40, 0));//22 => added
            timeIntervals.Add(date + new TimeSpan(16, 45, 0));//22 => added

            var resut = await this.feesService.GetTotalFee(timeIntervals, feesRangeLsit, vehicleType);

            Assert.AreEqual(total, resut);
        }
    }
}

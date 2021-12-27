using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TollCalculator.Common.DTOs;
using TollCalculator.Common.Helpers;
using TollCalculator.Services;
using TollCalculator.Services.Interfaces;

namespace TollCalculator.Test
{
    [TestClass]
    public class VehicleServiceTest
    {
        private  IVehicleService vehicleService;

        [TestInitialize]
        public void Init()
        {
            this.vehicleService = new VehicleService();
        }

        [TestMethod]
        public void VehicleService_SetVehicel_ReturensVehicleDTO()
        {
            VehicleDTO dto = new VehicleDTO() 
            {
                NumberPlate = "AAA",
                VehicleId = 1,
                VehicleType = 5
            };
            var result = this.vehicleService.SetVehicel();
            Assert.AreEqual(dto.VehicleId, result.VehicleId);
        }

        [TestMethod]
        public void VehicleService_IsTollApplicable_ReturensTrue()
        {
            int? vehicleType = 0;

            var result = this.vehicleService.IsTollApplicable(vehicleType);
            Assert.IsTrue (result);
        }

        [TestMethod]
        public void HoliDayHelper_IsAHoliday_ReturensTrue()
        {
            var date = DateTime.Parse("Jun 06, 2021");

            var result = HoliDayHelper.IsAHoliday (date);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HoliDayHelper_IsAWeekEnd_ReturensTrue()
        {
            var date = DateTime.Parse("Dec 18, 2021");

            var result = HoliDayHelper.IsAWeekEnd(date);
            Assert.IsTrue(result);
        }
    }
}

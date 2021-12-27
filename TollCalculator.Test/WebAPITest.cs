using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TollCalculator.Common.Enums;
using TollCalculator.Services.Interfaces;
using TollCalculator.Web.Controllers;
using TollCalculator.Web.Models;

namespace TollCalculator.Test
{
    [TestClass]
    public class WebAPITest
    {
        /// <summary>
        /// Input model is null
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FeesController_Post_NullFeeModel_ReturnResponseCode()
        {
            var mockFeeService = new Mock<IFeesService>();
            var webApiController = new FeesController(mockFeeService.Object);
            FeeModel feeModel = null;

            var response = await webApiController.Post(feeModel);
          
            Assert.AreEqual(response.ResponseCode, (int)ResonseEnum.Error);
        }

        /// <summary>
        /// Input model- date is empty is null
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FeesController_Post_EmptyDateFeeModel_ReturnResponseCode()
        {
            var mockFeeService = new Mock<IFeesService>();
            var webApiController = new FeesController(mockFeeService.Object);
            FeeModel feeModel = new FeeModel();
            feeModel.Date = string.Empty;
            feeModel.VehicleId = 22;
            feeModel.VehicleType = 20;

            var response = await webApiController.Post(feeModel);

            Assert.AreEqual(response.ResponseCode, (int)ResonseEnum.Error);
        }
    }
}

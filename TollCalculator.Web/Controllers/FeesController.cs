using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TollCalculator.Common.DTOs;
using TollCalculator.Common.Enums;
using TollCalculator.Services;
using TollCalculator.Services.Interfaces;
using TollCalculator.Web.Models;

namespace TollCalculator.Web.Controllers
{
    public class FeesController : ApiController
    {
        #region clas variables

        private readonly IFeesService feesService;
        private static readonly ILog Log = LogManager.GetLogger(typeof(FeesController));

        #endregion
        public FeesController(IFeesService _feesService)
        {
            this.feesService = _feesService;
        }

        #region API methods
        public async Task<ResponsModel> Post(FeeModel fees)
        {
            ResponsModel responsModel = new ResponsModel();

            try
            {
                if (fees == null)
                {
                    responsModel.ResponseCode = (int)ResonseEnum.Error;
                    Log.Error($"Model is null");
                    return  responsModel;
                }

                List<FeesRangeDTO> feesRanges = new List<FeesRangeDTO>();

                feesRanges = Mapper.Map<List<FeesRangeModel>, List<FeesRangeDTO>>(fees.FeesRangeList);

                List<DateTime> timeIntervals = new List<DateTime>();

                var date = DateTime.Parse(fees.Date);

                foreach (var interval in fees.TimeIntervalList)
                {
                    timeIntervals.Add(date + new TimeSpan(interval.Hours, interval.Minutes, 0));
                }

                var total = await this.feesService.GetTotalFee(timeIntervals, feesRanges, fees.VehicleType);
                responsModel.TotalFee = total;
                responsModel.ResponseCode = (int)ResonseEnum.success;
            }
            catch (Exception ex)
            {
                responsModel.ResponseCode = (int)ResonseEnum.Error;
                Log.Error($"Message =>{ex.Message} Description=> {ex.StackTrace}");
                return  responsModel;
            }

            return responsModel; 
        }

        #endregion
    }
}
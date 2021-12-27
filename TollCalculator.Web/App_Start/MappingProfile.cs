using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TollCalculator.Common.DTOs;
using TollCalculator.Web.Models;

namespace TollCalculator.Web.App_Start
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<FeesRangeModel, FeesRangeDTO>();
        }
    }
}
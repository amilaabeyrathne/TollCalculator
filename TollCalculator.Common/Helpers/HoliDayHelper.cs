using Nager.Date;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculator.Common.Helpers
{
    public static class HoliDayHelper
    {
        private static CountryCode countryCode = (CountryCode)(Convert.ToInt32(ConfigurationManager.AppSettings["CountryCode"]));
        public static bool IsAHoliday(DateTime date)
        {
            return DateSystem.IsPublicHoliday(date, countryCode);
        }
        
        public static bool IsAWeekEnd(DateTime date)
        {
            return DateSystem.IsWeekend(date, countryCode);
        }
    }
}

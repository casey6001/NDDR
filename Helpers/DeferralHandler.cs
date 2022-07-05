using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NDDR.Helpers
{
    public class DeferralHandler
    {
        private readonly IConfiguration _config;
        public DeferralHandler()
        {
            _config = ConfigProvider.Config;
        }

        public DateTime GetDefferalEndDate(InquiryDataDTO donorResult)
        {
            if (!string.IsNullOrEmpty(donorResult.LastBloodDonation) && !string.IsNullOrEmpty(donorResult.LastPlasmaDonation))
            {
                if (FormateDate(donorResult.LastBloodDonation).AddDays(Double.Parse(_config.GetSection("ValidityPeriods")["Blood"])) > FormateDate(donorResult.LastPlasmaDonation).AddDays(double.Parse(_config.GetSection("ValidityPeriods")["Plasma"])))
                {
                    return FormateDate(donorResult.LastBloodDonation).AddDays(double.Parse(_config.GetSection("ValidityPeriods")["Blood"]));
                }
                else
                    return FormateDate(donorResult.LastPlasmaDonation).AddDays(double.Parse(_config.GetSection("ValidityPeriods")["Plasma"]));
            }
            else
             return (!string.IsNullOrEmpty(donorResult.LastBloodDonation)) ?
             FormateDate(donorResult.LastBloodDonation).AddDays(double.Parse(_config.GetSection("ValidityPeriods")["Blood"]))
           : FormateDate(donorResult.LastPlasmaDonation).AddDays(double.Parse(_config.GetSection("ValidityPeriods")["Plasma"]));
          
        }

        private DateTime FormateDate (string unformattedDate)
        {
            //return DateTime.ParseExact(unformattedDate, "yyyy/mm/dd", CultureInfo.InvariantCulture);
            return Convert.ToDateTime(unformattedDate, CultureInfo.InvariantCulture);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDDR
{
    public class NDDRSevice : INDDRService
    {
      private  ContextFactory _contextFactory;
        public bool Index()
        {
            _contextFactory = new ContextFactory();
            _contextFactory.Execute("select * from centralized_plamsa  where national_id = '22501092401246'");
            return true;
        }
        public async Task<ServiceResult>  Inquiry(ServiceInquiryKeys serviceInquiryKeys)
        {
            _contextFactory =  new ContextFactory();

            var res = await _contextFactory.ExecuteNDDRInquiry("select permenant_deferred,last_donation,last_plasma_donation,deferal_end_date from centralized_plamsa  where national_id = '" + serviceInquiryKeys.donorIdNumber + "'");
            return res;   
           
        }

     }
}

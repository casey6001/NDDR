using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDDR.Controllers
{
    //[Route("api/[controller]")]
    public class NDDR : Controller
    {
        private readonly INDDRService _nDDRService;

        public NDDR(INDDRService nDDRService)
        {
            _nDDRService = nDDRService;
        }

        //[Authorize]
        // GET: NDDR
        public string[] Index()
        {
            _nDDRService.Index();
            return new string[] {"123" , "123"};
        }
        [Authorize]
        [HttpPost]
        [Route("api/v1/nddr/inquiry")]
        public async Task<ServiceResult> Inquiry([FromBody] ServiceInquiryKeys serviceInquiryKeys)
        {
            ServiceResult res = new ServiceResult();
            res.timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");
            res.inquiryReferenceNumber = DataHelper.RandomString(12);
            if (string.IsNullOrEmpty(serviceInquiryKeys.donorIdNumber))
            {
                res.inquiryResult = InquiryResult.ERR.ToString();
                 return res;
                
            }
            
            try
            {
                var inquiryRes = await _nDDRService.Inquiry(serviceInquiryKeys);
                
            res.inquiryResult = inquiryRes.inquiryResult;
            res.deferralEndDate = inquiryRes.deferralEndDate;

                 return res;
                
            }
            catch (Exception)
            {
                res.inquiryResult = InquiryResult.ERR.ToString();
                
                return res;
            
            }
          

        }

        [Authorize]
        [HttpPost]
        [Route("api/v1/cdcs/donations")]
        public async Task<ServiceResult> uploadDonorsData()
        {
            ServiceResult res = new ServiceResult();
          
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var y = await reader.ReadToEndAsync();
                }
                // var inquiryRes = await _nDDRService.Inquiry(serviceInquiryKeys);

                //res.inquiryResult = inquiryRes.inquiryResult;
                //res.deferralEndDate = inquiryRes.deferralEndDate;

                return res;

            }
            catch (Exception)
            {
                res.inquiryResult = InquiryResult.ERR.ToString();

                return res;

            }


        }

    }
}

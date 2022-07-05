using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDDR
{
    public class ServiceResult
    {
        public string inquiryReferenceNumber { get; set; }
        public string inquiryResult { get; set; }
        public string timeStamp { get; set; }
        public string deferralEndDate { get; set; }
    }
}

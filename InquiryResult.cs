using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NDDR
{
    public enum InquiryResult
    {
      [Description("MAT")]  MAT, //Indicates there was a MATCH and a deferral was found matching the search criteria
      [Description("CRO")]  CRO, //Indicates there was a MATCH and a cross donation was found matching the search criteria
      [Description("DAC")]  DAC, //Indicates there was a MATCH and a deferral and cross donation were found matching the search criteria
      [Description("NOT")]  NOT, //Indicates that a deferral/cross donation matching the search criteria was NOT FOUND in the registry
      [Description("ERR")] ERR  //Indicates that there was an error processing the Inquiry
    }
}

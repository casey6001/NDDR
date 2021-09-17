using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDDR
{
    public class AuthenticationActionResult : IActionResult
    {
        private readonly NDDRResult _result;

        public AuthenticationActionResult(NDDRResult result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result.Exception ?? _result.Data)
            {
                StatusCode = _result.Exception != null
                    ? StatusCodes.Status500InternalServerError
                    : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
    public class NDDRResult
    {
        public Exception Exception { get; set; }
        public object Data { get; set; }
    }
}

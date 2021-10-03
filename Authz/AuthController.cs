using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace NDDR
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }


      
        [AllowAnonymous]
        [HttpPost("connect")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _service.Authenticate(login);
            if (result == null) return BadRequest();
        
           // return Ok(new { TransferEncoding = result });
            var statusCode = HttpStatusCode.OK;
            return StatusCode((int)statusCode, result);
        }


       
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
      }
}
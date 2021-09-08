using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDDR
{
  public  interface IAuthService
    {
        /// <summary>
        ///     Login to the system
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        Task<TokenDto> Authenticate(LoginDto model);

      }
}

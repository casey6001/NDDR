using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;


namespace NDDR
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private ContextFactory _contextFactory;

        public AuthService(
            
            IConfiguration configuration,
                      IHttpContextAccessor httpContext
        )

        {
        
            _configuration = configuration;
            _httpContext = httpContext;

        }

        public async Task<TokenDto> Authenticate(LoginDto model)
        {
            _contextFactory = new ContextFactory();
            var user = _contextFactory.FIndUserInquiry(model);

            
            if (user != null)
            {
                // authentication successful so generate jwt and refresh tokens
               return await GenerateJwtToken(user);
           
            }

            return null;
        }

    

    

        // helper methods

        private async Task<TokenDto> GenerateJwtToken(User user)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            //var userData = _mapper.Map<UserDto>(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDto()
            {
                access_token = tokenHandler.WriteToken(token),
                expires_in = tokenDescriptor.Expires.ToString(),
                token_type = "Bearer",
                scope      = "api.nddr"               

            };
            
        }

       

    

    }
}
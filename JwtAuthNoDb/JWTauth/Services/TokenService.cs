using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTauth.Interfaces;
using JWTauth.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTauth.Services
{
    public class TokenService:ITokenService
    {
         readonly IConfiguration configuration;

         public TokenService(IConfiguration configuration)
         {
             this.configuration = configuration;

         }
        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));
            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                
                issuer: configuration["JWT:ValidIssuer"],/*token kimler tarafından kullanacak (www.yusuf.com)*/
                audience:configuration["JWT:ValidAudience"],/*token kim tarafında dağıtılıyor (yani biz yusuf)*/
                claims: new List<Claim>
                {
                    new Claim("userName",request.Username)//token içinde saklamak istediğimiz bilgiler
                },
                notBefore: dateTimeNow,
                expires:dateTimeNow.Add(TimeSpan.FromMinutes(500)),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            return Task.FromResult(new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(500))
            });
        }
    }
}
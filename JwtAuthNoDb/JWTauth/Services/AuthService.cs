using System;
using System.Threading.Tasks;
using JWTauth.Interfaces;
using JWTauth.Models;

namespace JWTauth.Services
{
    public class AuthService :IAuthService
    {
         readonly ITokenService tokenService;

         public AuthService(ITokenService tokenService)
         {
             this.tokenService = tokenService;
         }
        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse();

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Username == "yusuf" && request.Password == "123456")
            {
                var generatedTokenInformation = await tokenService.GenerateToken(new GenerateTokenRequest
                    { Username = request.Username });
               
                response.AuthenticateResult = true;
                response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
                response.AuthToken = generatedTokenInformation.Token;
            }

            return response;
        }

    }
}
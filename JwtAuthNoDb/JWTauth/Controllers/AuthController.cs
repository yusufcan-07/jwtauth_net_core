using System.Threading.Tasks;
using JWTauth.Interfaces;
using JWTauth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        readonly IAuthService AuthService;

        public AuthController(IAuthService authService)
        {
            this.AuthService = authService;
            
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> LoginuserAsync([FromBody] UserLoginRequest request)
        {
            var result = await AuthService.LoginUserAsync(request);
            return result;
        }

    }
}
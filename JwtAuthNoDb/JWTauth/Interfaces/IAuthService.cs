using System.Threading.Tasks;
using JWTauth.Models;

namespace JWTauth.Interfaces
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);

    }
}
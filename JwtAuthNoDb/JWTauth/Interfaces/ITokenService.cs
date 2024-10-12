using System.Threading.Tasks;
using JWTauth.Models;

namespace JWTauth.Interfaces
{
    public interface ITokenService
    {
        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request);

    }
}
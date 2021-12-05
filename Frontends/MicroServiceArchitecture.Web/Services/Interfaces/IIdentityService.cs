using IdentityModel.Client;
using MicroServiceArchitecture.Shared.Dtos;
using MicroServiceArchitecture.Web.Models;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignInAsync(SigninInput input);

        Task<TokenResponse> GetAccessTokenByRefreshTokenAsync();

        Task RevokeRefreshTokenAsync();
    }
}

using System;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetTokenAsync();
    }
}

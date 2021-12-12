using MicroServiceArchitecture.Web.Models;
using MicroServiceArchitecture.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Methods

        public async Task<UserViewModel> GetUserAsync()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
        }

        #endregion
    }
}

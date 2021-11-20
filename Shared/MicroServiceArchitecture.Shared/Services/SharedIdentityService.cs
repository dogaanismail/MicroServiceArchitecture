using Microsoft.AspNetCore.Http;

namespace MicroServiceArchitecture.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        #region Fields
        private IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor
        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;

        #endregion
    }
}

using MicroServiceArchitecture.Web.Models;
using Microsoft.Extensions.Options;

namespace MicroServiceArchitecture.Web.Helpers
{
    public class PhotoHelper
    {
        #region Fields
        private readonly ServiceApiSettings _serviceApiSettings;

        #endregion

        #region Ctor

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        #endregion

        #region Methods

        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }

        #endregion
    }
}

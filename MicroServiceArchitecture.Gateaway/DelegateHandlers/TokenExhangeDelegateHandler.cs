using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Gateaway.DelegateHandlers
{
    public class TokenExhangeDelegateHandler : DelegatingHandler
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _accessToken;

        #endregion

        #region Ctor

        public TokenExhangeDelegateHandler(HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        #endregion

        #region Methods

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestToken = request.Headers.Authorization.Parameter;

            var newToken = await GetTokenAsync(requestToken);

            request.SetBearerToken(newToken);

            return await base.SendAsync(request, cancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task<string> GetTokenAsync(string requestToken)
        {
            if (!string.IsNullOrEmpty(_accessToken))
                return _accessToken;

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration["IdentityServerURL"],
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
                throw disco.Exception;

            TokenExchangeTokenRequest tokenExchangeTokenRequest = new()
            {
                Address = disco.TokenEndpoint,
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"],
                GrantType = _configuration["TokenGrantType"],
                SubjectToken = requestToken,
                SubjectTokenType = "urn:ietf:params:oauth:token-type:access-token",
                Scope = "openid discount_fullpermission payment_fullpermission"
            };

            var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);

            if (tokenResponse.IsError)
                throw tokenResponse.Exception;

            _accessToken = tokenResponse.AccessToken;

            return _accessToken ?? string.Empty;
        }

        #endregion
    }
}

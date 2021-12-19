﻿using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using MicroServiceArchitecture.Web.Models;
using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        #region Fields

        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly HttpClient _httpClient;
        #endregion

        #region Ctor

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings,
            IOptions<ClientSettings> clientSettings,
            IClientAccessTokenCache clientAccessTokenCache,
            HttpClient httpClient)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        #endregion

        #region Methods

        public async Task<string> GetTokenAsync()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken", new ClientAccessTokenParameters { });

            if (currentToken != null)
                return currentToken.AccessToken;

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
                throw disco.Exception;

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn, new ClientAccessTokenParameters { });

            return newToken.AccessToken;
        }

        #endregion
    }
}

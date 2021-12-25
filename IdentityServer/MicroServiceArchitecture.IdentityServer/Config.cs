using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace MicroServiceArchitecture.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes = {"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes = {"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            new ApiResource("resource_basket"){Scopes = {"basket_fullpermission"}},
            new ApiResource("resource_discount"){Scopes = {"discount_fullpermission"}},
            new ApiResource("resource_order"){Scopes = {"order_fullpermission"}},
            new ApiResource("resource_payment"){Scopes = {"payment_fullpermission"}},
            new ApiResource("resource_gateway"){Scopes = {"gateway_fullpermission"}}
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){
                           Name="roles",
                           DisplayName="Roles",
                           Description="User roles",
                           UserClaims = new[]{"role"} }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Catalog api icin full erisim yetkisi."),
                new ApiScope("photo_stock_fullpermission", "Photo Stock api icin full erisim yetkisi."),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope("basket_fullpermission", "Basket api icin full erisim yetkisi."),
                new ApiScope("discount_fullpermission", "Discount api icin full erisim yetkisi."),
                new ApiScope("order_fullpermission", "Order api icin full erisim yetkisi."),
                new ApiScope("payment_fullpermission", "Payment api icin full erisim yetkisi."),
                new ApiScope("gateway_fullpermission", "Gateway api icin full erisim yetkisi.")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        "catalog_fullpermission",
                        "photo_stock_fullpermission",
                        "gateway_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName }
                },

                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                      "basket_fullpermission",
                      "order_fullpermission",
                      "gateway_fullpermission",
                      IdentityServerConstants.StandardScopes.Email,
                      IdentityServerConstants.StandardScopes.OpenId,
                      IdentityServerConstants.StandardScopes.Profile,
                      IdentityServerConstants.StandardScopes.OfflineAccess,
                      "roles",
                      IdentityServerConstants.LocalApi.ScopeName
                    },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },

                new Client
                {
                    ClientName = "Token Exchange Client",
                    ClientId = "TokenExhangeClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = new []{ "urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes =
                    {
                     "discount_fullpermission",
                     "payment_fullpermission",
                     IdentityServerConstants.StandardScopes.OpenId,
                    }
                }
            };
    }
}
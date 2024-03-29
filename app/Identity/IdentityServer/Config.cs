﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using System.Security.Claims;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("CatalogAPI"),

            new ApiScope("BasketAPI"),

            new ApiScope("WebBffAPI"),

            new ApiScope
            {
                Name = "OrderAPI",
                DisplayName = "Order API",
                UserClaims = new List<string> 
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email 
                }
            }
        };

    public static IEnumerable<Client> Clients =>
     new List<Client>
     {

         //Catalog API Client
        new Client
        {
            ClientId = "catalog_api_swagger",
            ClientName = "Swagger UI for Catalog API",
            ClientSecrets = { new Secret("catalog_api_secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.Implicit, 

            RedirectUris = { "http://localhost:5000/swagger/oauth2-redirect.html" },
            AllowedCorsOrigins = { "http://localhost:5000" },
            AllowedScopes = new List<string>
            {
                "CatalogAPI"
            },

            AllowAccessTokensViaBrowser = true, 
        },

        new Client
        {
            ClientId = "catalog_api_client",
            ClientName = "Client for Catalog API",
            ClientSecrets = { new Secret("catalog_api_client_secret".Sha256()) },

             AllowedGrantTypes = GrantTypes.ClientCredentials, 

            AllowedScopes = new List<string>
            {
                "CatalogAPI"
            },
        },

        //Basket API Client
        new Client
        {
            ClientId = "basket_api_swagger",
            ClientName = "Swagger UI for Basket API",
            ClientSecrets = { new Secret("basket_api_secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.Implicit,

            RedirectUris = { "http://localhost:5004/swagger/oauth2-redirect.html" },
            AllowedCorsOrigins = { "http://localhost:5004" },
            AllowedScopes = new List<string>
            {
                "BasketAPI"
            },
            AllowAccessTokensViaBrowser = true,

            Claims = new List<ClientClaim>
            {
                new ClientClaim(ClaimTypes.NameIdentifier, "userId")
            }


        },

        new Client
        {
            ClientId = "basket_api_client",
            ClientName = "Client for Basket API",
            ClientSecrets = { new Secret("basket_api_client_secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.ClientCredentials,

            AllowedScopes = new List<string>
            {
                "BasketAPI",
            },

             Claims = new List<ClientClaim>
            {
                new ClientClaim(ClaimTypes.NameIdentifier, "userId")
            }

        },

        //Ordering Api Client
        new Client
        {
            ClientId = "order_api_swagger",
            ClientName = "Swagger UI for Ordering API",
            ClientSecrets = { new Secret("order_api_secret".Sha256()) },

            

            AllowedGrantTypes = GrantTypes.Implicit,

            RedirectUris = { "http://localhost:5005/swagger/oauth2-redirect.html" },
            AllowedCorsOrigins = { "http://localhost:5005" },
            AllowedScopes = new List<string>
            {
                "OrderAPI",
            },
            AllowAccessTokensViaBrowser = true,
            AlwaysIncludeUserClaimsInIdToken = true,

            Claims = new List<ClientClaim>
            {
                 new ClientClaim(JwtClaimTypes.Name, "name"),
                 new ClientClaim(JwtClaimTypes.Email, "email")
            }

        },

        new Client
        {
            ClientId = "order_api_client",
            ClientName = "Client for Orderomg API",
            ClientSecrets = { new Secret("order_api_client_secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.ClientCredentials,

            AllowedScopes = new List<string>
            {
                "OrderAPI"
            }
        },

        // Web Bff Api Client
        new Client
        {
            ClientId = "webbff_api_swagger",
            ClientName = "Swagger UI for Web Bff API",
            ClientSecrets = {new Secret("webbff_api_secret".Sha256())},

            AllowedGrantTypes = GrantTypes.Code,

            RedirectUris = { "http://localhost:5002/swagger/oauth2-redirect.html" },

            AllowedCorsOrigins = { "http://localhost:5002" },
            AllowedScopes = new List<string>
            {
                "WebBffAPI",
            },
        },

        // WebApp MVC Client
        new Client
         {
             ClientId = "mvc_client",
             ClientName = "MVC Client",
             ClientSecrets = { new Secret("mvc_secret".Sha256()) },

             AllowedGrantTypes = new[]
             {
                 GrantType.ResourceOwnerPassword,
                 GrantType.ClientCredentials,
             },

             RedirectUris = { "http://localhost:5003/signin-oidc" },
             FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
             PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

             AllowedScopes = new List<string>
             {
                 "openid",
                 "profile",
                 "CatalogAPI",
                 "WebBffAPI"
             },

             AllowOfflineAccess = true
         }
     };
}

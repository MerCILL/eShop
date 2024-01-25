using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("CatalogAPI"),

            new ApiScope("WebBffAPI"),
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
                "WebBffAPI"
            }
        },

        // NextJs Client
        new Client
        {
            ClientId = "nextjs_web_app",
            ClientName = "NextJs Web App",
            ClientSecrets = { new Secret("secret".Sha256()) },

            AllowedGrantTypes = new[] { GrantType.AuthorizationCode },

            RedirectUris = { "http://localhost:3000/api/auth/callback/sample-identity-server" },
            PostLogoutRedirectUris = { "http://localhost:3000" },

            AllowedCorsOrigins= { "http://localhost:3000" },
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "WebBffAPI"
            },
        },
     };
}

using Catalog.API.Responses;
using Catalog.Domain.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http;

namespace BFF.Web.Controllers;

[ApiController]
[Route("/bff/catalog")]
[Authorize(Policy = "ApiScope")]
public class WebBffController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WebBffController> _logger;

    public WebBffController(IHttpClientFactory httpClientFactory, ILogger<WebBffController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                _logger.LogError(disco.Error);
                return StatusCode(500); 
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "catalog_api_client",
                ClientSecret = "catalog_api_client_secret",
                Scope = "CatalogAPI"
            });

            if (tokenResponse.IsError)
            {
                _logger.LogError(tokenResponse.Error);
                return StatusCode(500); 
            }

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"http://localhost:5000/api/v1/catalog/brands?page={page}&size={size}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var brands = JsonConvert.DeserializeObject<PaginatedResponse<CatalogBrand>>(content);
                return Ok(brands);
            }
            else
            {
                _logger.LogError($"WebBff API request failed with status code: {response.StatusCode}");
                return StatusCode((int)response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred in GetBrands: {ex.Message}");
            return StatusCode(500); 
        }
    }
}
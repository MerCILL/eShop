using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using IdentityModel.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public WeatherForecastController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Authorize]
        public async Task<IActionResult> WeatherForecast()
        {
            var httpClient = _clientFactory.CreateClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                return View("Error");
            }

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "mvc_client",
                ClientSecret = "mvc_secret",
                Scope = "WebBffAPI"
            });

            if (tokenResponse.IsError)
            {
                return View("Error");
            }

            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await httpClient.GetAsync("http://localhost:5002/weather");

            if (response.IsSuccessStatusCode)
            {
                var weatherForecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
                return View(weatherForecasts);
            }
            else
            {
                return View("Error");
            }
        }

    }
}
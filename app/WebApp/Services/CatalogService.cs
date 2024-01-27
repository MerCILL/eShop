﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Services;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientFactory _clientFactory;

    public CatalogService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<PaginatedResponse<CatalogItemModel>> GetCatalogItems(int page, int size, string sort)
    {
        var httpClient = _clientFactory.CreateClient();

        var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
        if (disco.IsError)
        {
            throw new Exception("Discovery document error");
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
            throw new Exception("Token request error");
        }

        httpClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await httpClient.GetAsync($"http://localhost:5002/bff/catalog/items?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedResponse<CatalogItemModel>>(content);

            switch (sort)
            {
                case "price_asc":
                    result.Data = result.Data.OrderBy(item => item.Price).ToList();
                    break;
                case "price_desc":
                    result.Data = result.Data.OrderByDescending(item => item.Price).ToList();
                    break;
                case "name_asc":
                    result.Data = result.Data.OrderBy(item => item.Title).ToList();
                    break;
                case "name_desc":
                    result.Data = result.Data.OrderByDescending(item => item.Title).ToList();
                    break;
                case "date_asc":
                    result.Data = result.Data.OrderBy(item => item.CreatedAt).ToList();
                    break;
                case "date_desc":
                    result.Data = result.Data.OrderByDescending(item => item.CreatedAt).ToList();
                    break;
            }

            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }
}
using AutoMapper;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Core.Abstractions.Repositories;
using Ordering.DataAccess.Entities;
using Ordering.Domain.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Ordering.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository<OrderEntity> _orderRepository;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUserService _userService;

    public OrderService(
        IOrderRepository<OrderEntity> orderRepository,
        IMapper mapper,
        IHttpClientFactory httpClientFactory,
        IUserService userService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
        _userService = userService;
    }

    public async Task<IEnumerable<Order>> Get(int page, int size)
    {
        var ordersEntities = await _orderRepository.Get(page, size);

        var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);

        return orders;
    }

    public async Task<IEnumerable<Order>> GetByUser(string userId, int page, int size)
    {
        var ordersEntities = await _orderRepository.GetByUser(userId, page, size);

        var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);

        return orders;
    }

    public async Task<Order> GeyById(int id)
    {
        var orderEntity = await _orderRepository.GetById(id);

        var order = _mapper.Map<Order>(orderEntity);

        return order;
    }

    public async Task<Order> Add(Order order, ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var getBasketResponse = await apiClient.GetAsync($"http://localhost:5004/api/v1/basket/{userId}");

        var getBasketcontent = await getBasketResponse.Content.ReadAsStringAsync();
        var basket = JsonConvert.DeserializeObject<Basket>(getBasketcontent);

        if (basket.Items.Count == 0 || basket.Items == null)
        {
            throw new Exception("Basket is empty");
        }

        var orderEntity = _mapper.Map<OrderEntity>(order);
        orderEntity.UserId = userId;
        orderEntity.OrderDate = DateTime.UtcNow;
        orderEntity.Items = basket.Items.Select(item => new OrderItemEntity
        {
            ItemId = item.ItemId,
            Title = item.ItemTitle,
            Price = item.ItemPrice,
            Quantity = item.Quantity,
            PictureUrl = item.PictureUrl,
        }).ToList();

        var createdOrder = await _orderRepository.Add(orderEntity);

        return _mapper.Map<Order>(createdOrder);
    }
}



//var deleteBasketResponse = await apiClient.DeleteAsync($"http://localhost:5004/api/v1/basket/{orderEntity.UserId}");

//var user = _userService.Add(userClaims);

public class Basket
{
    public string UserId { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public decimal TotalPrice
    {
        get => Items.Sum(item => item.ItemPrice);
    }
    public int TotalCount
    {
        get => Items.Sum(item => item.Quantity);
    }
}

public class BasketItem
{
    public int ItemId { get; set; }
    public string ItemTitle { get; set; }
    public decimal ItemPrice { get; set; }
    public string PictureUrl { get; set; }
    public int Quantity { get; set; }
}
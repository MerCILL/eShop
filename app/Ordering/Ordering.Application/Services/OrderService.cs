using Helpers;
using Microsoft.Extensions.Options;
using Ordering.Application.Infrastructure.Settings;

namespace Ordering.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository<OrderEntity> _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITransactionService _transactionService;
    private readonly ApiClientSettings _catalogSettings;
    private readonly ApiClientSettings _basketSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public OrderService(
        IOrderRepository<OrderEntity> orderRepository,
        IMapper mapper,
        IUserService userService,
        ITransactionService transactionService,
        IOptions<CatalogApiClientSettings> catalogSettings,
        IOptions<BasketApiClientSettings> basketSettings,
        ApiClientHelper apiClientHelper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userService = userService;
        _transactionService = transactionService;
        _catalogSettings = catalogSettings.Value;
        _basketSettings = basketSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<IEnumerable<Order>> Get(int page, int size)
    {
        var ordersEntities = await _orderRepository.Get(page, size);

        var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);

        return orders;
    }

    public async Task<Order> GeyById(int id)
    {
        var orderEntity = await _orderRepository.GetById(id);

        var order = _mapper.Map<Order>(orderEntity);

        return order;
    }

    public async Task<IEnumerable<Order>> GetByUser(string userId, int page, int size)
    {
        var ordersEntities = await _orderRepository.GetByUser(userId, page, size);

        var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);

        return orders;
    }


    public async Task<Order> Add(Order order, string userId)
    {
        Order createdOrder = null;
        await _transactionService.ExecuteInTransactionAsync(async () =>
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var basket = await GetBasketByUserId(userId);

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

            orderEntity = await _orderRepository.Add(orderEntity);

            var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
            var deleteBasketResponse = await apiClient.DeleteAsync($"{_basketSettings.ApiUrl}/{orderEntity.UserId}");

            createdOrder = _mapper.Map<Order>(orderEntity);
        });

        return createdOrder;
    }


    public async Task<Order> Update(Order order, ClaimsPrincipal userClaims)
    {
        var currentOrder = await _orderRepository.GetById(order.Id);
        if (currentOrder == null)
        {
            throw new KeyNotFoundException("Order not found.");
        }

        currentOrder.Address = order.Address;

        foreach (var item in order.Items)
        {
            var catalogItem = await GetCatalogItemById(item.ItemId);
            if (catalogItem == null)
            {
                throw new KeyNotFoundException($"Item with id {item.ItemId} not found in catalog.");
            }

            var currentOrderItem = currentOrder.Items.FirstOrDefault(i => i.Id == item.Id);
            if (currentOrderItem != null)
            {
                currentOrderItem.Quantity = item.Quantity;
            }
            else
            {
                var orderItemEntity = _mapper.Map<OrderItemEntity>(item);
                orderItemEntity.Title = catalogItem.Title;
                orderItemEntity.PictureUrl = catalogItem.PictureFile;
                orderItemEntity.Price = catalogItem.Price;
                currentOrder.Items.Add(orderItemEntity);
            }
        }

        currentOrder.Items.RemoveAll(item => !order.Items.Any(i => i.Id == item.Id));

        await _orderRepository.Update(currentOrder);

        return _mapper.Map<Order>(currentOrder);
    }

    public async Task<Order> Delete(int id)
    {
        var orderEntity = await _orderRepository.Delete(id);
        return _mapper.Map<Order>(orderEntity);
    }

    private async Task<CatalogItem> GetCatalogItemById(int id)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettings);
        var response = await apiClient.GetAsync($"{_catalogSettings.ApiUrl}/items/{id}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CatalogItem>(content);
        return result;
    }

    private async Task<Basket> GetBasketByUserId(string userId)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
        var response = await apiClient.GetAsync($"{_basketSettings.ApiUrl}/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Basket>(content);
        return result;
    }
}



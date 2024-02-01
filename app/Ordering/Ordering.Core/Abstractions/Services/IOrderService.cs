using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.Application.Services;

public interface IOrderService
{
    Task<Order> Add(Order order, ClaimsPrincipal userClaims);
    Task<IEnumerable<Order>> Get(int page, int size);
    Task<IEnumerable<Order>> GetByUser(string userId, int page, int size);
    Task<Order> GeyById(int id);
}
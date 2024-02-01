using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Requests;
using Ordering.Application.Services;
using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GeyById(id);
        if (order != null)
        {
            return Ok(order);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
    {
        try
        {
            var userClaims = User;

            var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = userClaims.FindFirst(JwtClaimTypes.Name)?.Value;
            var userEmail = userClaims.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            var order = new Order
            {
                User = new User
                {
                    UserId = userId,
                    UserName = userName,
                    UserEmail = userEmail
                },
                Address = orderRequest.Address,
                OrderDate = DateTime.UtcNow
            };

            var result = await _orderService.Add(order, userClaims);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }      
    }
}

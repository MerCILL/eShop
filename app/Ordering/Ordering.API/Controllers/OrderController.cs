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
        var order = new Order { Address = orderRequest.Address };

        var createdOrder = await _orderService.Add(order, User);

        return Ok(createdOrder.Id);
    }
}

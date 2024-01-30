﻿using BFF.Web.Responses;

namespace BFF.Web.Services.Abstractions;

public interface IBasketService
{
    Task<Basket> GetBasket(string userId);
}
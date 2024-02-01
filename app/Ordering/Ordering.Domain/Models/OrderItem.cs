﻿namespace Ordering.Domain.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; }
}
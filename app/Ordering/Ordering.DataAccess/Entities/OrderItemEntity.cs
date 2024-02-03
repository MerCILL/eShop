﻿namespace Ordering.DataAccess.Entities;

public class OrderItemEntity
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public OrderEntity Order { get; set; }
}
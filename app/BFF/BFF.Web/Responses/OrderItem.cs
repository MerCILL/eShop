﻿namespace BFF.Web.Responses;

public class OrderItem
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
}

namespace BFF.Web.Responses;

public class Basket
{
    public string UserId { get; set; } = string.Empty;
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public decimal TotalPrice { get; set; }
    public int TotalCount { get; set; } 
}

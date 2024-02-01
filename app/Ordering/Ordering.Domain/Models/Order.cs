namespace Ordering.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Address { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}

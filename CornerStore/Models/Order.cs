namespace CornerStore.Models;

public class Order
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public Cashier Cashier { get; set; }
    public List<OrderProduct> OrderProducts { get; set; } = new();
    public decimal Total => OrderProducts?.Sum(op => (op?.Product?.Price ?? 0) * op.Quantity) ?? 0;

    public DateTime? PaidOnDate { get; set; }
}

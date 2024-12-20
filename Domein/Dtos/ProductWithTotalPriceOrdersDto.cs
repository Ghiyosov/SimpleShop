using Domein.Models;

namespace Domein.Dtos;

public class ProductWithTotalPriceOrdersDto
{
    public int ProductId { get; set; }
    public int ProductName { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public decimal TotalPriceOrders { get; set; }
    public List<Order> Orders { get; set; } = [];
}
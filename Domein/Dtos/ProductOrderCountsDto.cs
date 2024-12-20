namespace Domein.Dtos;

public class ProductOrderCountsDto
{
    public int ProductId { get; set; }
    public int ProductName { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int OrderCount { get; set; }
}
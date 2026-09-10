namespace SalesApi.Models;

public class Sale
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}
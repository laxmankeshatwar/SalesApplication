namespace SalesImporter
{
    public class Sale
    {
        public string OrderId { get; set; } = default!;
        public string Product { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
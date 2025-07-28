namespace Basket.DTOs
{
    public record CreateShoppingCartItemDto 
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageFile { get; set; }
    }
}

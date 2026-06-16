namespace FinFlow.Modules.Items.Items.Model.Classes
{
    public class ItemResponse : IItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
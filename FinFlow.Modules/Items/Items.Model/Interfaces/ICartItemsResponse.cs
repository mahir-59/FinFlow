namespace FinFlow.Modules.Items.Items.Model.Interfaces
{
    public interface ICartItemsResponse
    {
        public string Id { get; set; }
        public string CartId { get; set; }
        public string ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
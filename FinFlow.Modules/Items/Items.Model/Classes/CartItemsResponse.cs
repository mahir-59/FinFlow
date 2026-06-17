using FinFlow.Modules.Items.Items.Model.Interfaces;

namespace FinFlow.Modules.Items.Items.Model.Classes
{
    public class CartItemsResponse : ICartItemsResponse
    {
        public string Id { get; set; }
        public string CartId { get; set; }
        public string ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
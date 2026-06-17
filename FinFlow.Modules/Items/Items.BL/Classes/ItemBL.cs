using FinFlow.Modules.Items.Items.BL.Interfaces;
using FinFlow.Modules.Items.Items.DL.Classes;
using FinFlow.Modules.Items.Items.DL.Interfaces;
using FinFlow.Modules.Items.Items.Model.Classes;
using FinFlow.Modules.Items.Items.Model.Interfaces;

namespace FinFlow.Modules.Items.Items.BL.Classes
{
    public class ItemBL : IItemBL
    {
        private readonly IItemDL _itemDL;
        public ItemBL(IItemDL itemDL)
        {
            _itemDL = itemDL;
        }
        public async Task<List<IItemResponse>> GetAllItems()
        {
            return await _itemDL.GetAllItems();
        }
    }
}
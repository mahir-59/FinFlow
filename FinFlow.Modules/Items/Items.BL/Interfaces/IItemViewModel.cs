using FinFlow.Modules.Items.Items.Model.Classes;

namespace FinFlow.Modules.Items.Items.BL.Interfaces
{
    public interface IItemViewModel
    {
        Task<List<ItemResponse>> GetAllItems();
    }
}
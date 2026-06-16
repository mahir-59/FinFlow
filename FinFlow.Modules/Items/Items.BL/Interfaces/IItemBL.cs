
using FinFlow.Modules.Items.Items.DL.Classes;
using FinFlow.Modules.Items.Items.Model.Classes;

namespace FinFlow.Modules.Items.Items.BL.Interfaces
{
    public interface IItemBL
    {
        Task<List<IItemResponse>> GetAllItems();
    }
}
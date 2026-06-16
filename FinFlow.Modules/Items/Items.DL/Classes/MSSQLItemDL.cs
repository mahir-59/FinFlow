using FinFlow.Modules.Common.Helpers;
using FinFlow.Modules.Items.Items.DL.Interfaces;
using FinFlow.Modules.Items.Items.Model.Classes;

namespace FinFlow.Modules.Items.Items.DL.Classes
{
    public class MSSQLItemDL : IItemDL
    {
        private readonly SqlHelper _sqlHelper;
        public MSSQLItemDL( SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public async Task<List<IItemResponse>> GetAllItems()
        {
            try
            {
                var sql = "Select * from Items";
                return (await _sqlHelper.QueryAsync<IItemResponse>(sql)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
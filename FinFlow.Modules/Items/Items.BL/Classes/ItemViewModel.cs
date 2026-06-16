using FinFlow.Modules.Base.Base.BL;
using FinFlow.Modules.Base.Base.Model;
using FinFlow.Modules.Items.Items.BL.Interfaces;
using FinFlow.Modules.Items.Items.Model.Classes;
using Microsoft.Extensions.Configuration;

namespace FinFlow.Modules.Items.Items.BL.Classes
{
    public class ItemViewModel : IItemViewModel
    {
        private APIRequestHandler _apiRequestHandler { get; set;  }
        private IConfiguration _configuration { get; set; }
        private TokenStore _tokenStore { get; set; }
        private readonly string _apiBaseUrl;
        public ItemViewModel(APIRequestHandler aPIRequestHandler, TokenStore tokenStore, IConfiguration configuration)
        {
            _apiRequestHandler = aPIRequestHandler;
            _tokenStore = tokenStore;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://finflow-bbc3hxbshtf9c6h9.southindia-01.azurewebsites.net";
        }

        public async Task<List<ItemResponse>> GetAllItems()
        {
            var response = await _apiRequestHandler.SendAsync<List<ItemResponse>>(
                $"{_apiBaseUrl}/api/Items",
                HttpMethod.Get,
                null);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            return new List<ItemResponse>();
        }
    }
    
}
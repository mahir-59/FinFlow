using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using FinFlow.Modules.Auth.Auth.Model.Classes.Responses;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Responses;
using FinFlow.Modules.Base.Base.BL;
using FinFlow.Modules.Base.Base.Model;
using FinFlow.Modules.Common.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.BL.Classes
{
    public class LoginViewModel : ILoginViewModel
    {
        private APIRequestHandler _apiRequestHandler { get; set;  }
        private IConfiguration _configuration { get; set; }
        private TokenStore _tokenStore { get; set; }

        private readonly string _apiBaseUrl;
        public LoginViewModel(APIRequestHandler aPIRequestHandler, TokenStore tokenStore, IConfiguration configuration)
        {
            _apiRequestHandler = aPIRequestHandler;
            _tokenStore = tokenStore;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7281";
        }

        public async Task<bool> HandleLogin(ILoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return false;
            }

            var response = await _apiRequestHandler.SendAsync<LoginResponse>($"{_apiBaseUrl}/api/Auth/login", HttpMethod.Post, loginRequest);
            if(response.IsSuccess)
            {
                var data = response.Data;
                _tokenStore.AccessToken = data.AccessToken;
                _tokenStore.RefreshToken = data.RefreshToken;
                return true;
            }
            return false;
        }
    }
}

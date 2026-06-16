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
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://finflow-bbc3hxbshtf9c6h9.southindia-01.azurewebsites.net";
        }

        public async Task<GenericResponse> HandleLogin(ILoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return new GenericResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request"
                };
            }

            var response = await _apiRequestHandler.SendAsync<LoginResponse>(
                $"{_apiBaseUrl}/api/Auth/login",
                HttpMethod.Post,
                loginRequest);

            if (response.IsSuccess)
            {
                var data = response.Data;

                _tokenStore.AccessToken = data.AccessToken;
                _tokenStore.RefreshToken = data.RefreshToken;

                return new GenericResponse
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Message = "Login Successful"
                };
            }

            return new GenericResponse
            {
                IsSuccess = false,
                StatusCode = response.StatusCode,
                Message = response.Message
            };
        }
    }
}

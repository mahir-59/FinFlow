using FinFlow.Modules.Base.Base.Model;
using FinFlow.Modules.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinFlow.Modules.Base.Base.BL
{
    public class APIRequestHandler
    {
        private readonly HttpClient _httpClient;
        private readonly TokenStore _tokenStore;

        public APIRequestHandler(HttpClient httpClient, TokenStore tokenStore)
        {
            _httpClient = httpClient;
            _tokenStore = tokenStore;
        }

        public async Task<ApiResponse<T>> SendAsync<T>(string url, HttpMethod method, object? body = null, bool retry = true)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);

                // Token
                if (!string.IsNullOrWhiteSpace(_tokenStore.AccessToken))
                {
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue(
                            "Bearer",
                            _tokenStore.AccessToken);
                }

                // Body
                if (body is not null)
                {
                    var json = JsonSerializer.Serialize(body);

                    request.Content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");
                }

                // API Call
                var response = await _httpClient.SendAsync(request);

                // Unauthorized
                if (response.StatusCode == HttpStatusCode.Unauthorized && retry)
                {
                    var refreshSuccess = await RefreshTokenAsync();

                    if (refreshSuccess)
                    {
                        return await SendAsync<T>(
                            url,
                            method,
                            body,
                            false);
                    }

                    Logout();

                    return new ApiResponse<T>
                    {
                        IsSuccess = false,
                        StatusCode = 401,
                        Message = APIResponseConstants.SessionExpired
                    };
                }

                // Read Raw Response
                var rawResponse =
                    await response.Content.ReadAsStringAsync();

                // Failed
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<T>
                    {
                        IsSuccess = false,
                        StatusCode = (int)response.StatusCode,
                        Message = rawResponse
                    };
                }

                // Deserialize Success Data
                var apiResponse =
                    JsonSerializer.Deserialize<ApiResponse<T>>(
                        rawResponse,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return apiResponse!; ;
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
        private async Task<bool> RefreshTokenAsync()
        {
            try
            {
                var refreshRequest = new
                {
                    AccessToken = _tokenStore.AccessToken,
                    RefreshToken = _tokenStore.RefreshToken
                };

                var json = JsonSerializer.Serialize(refreshRequest);

                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "api/auth/refresh-token");

                request.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return false;

                var tokenResponse =
                    await response.Content
                        .ReadFromJsonAsync<TokenResponse>();

                if (tokenResponse is null)
                    return false;

                // Update Shared Tokens
                _tokenStore.AccessToken =
                    tokenResponse.AccessToken;

                _tokenStore.RefreshToken =
                    tokenResponse.RefreshToken;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Logout()
        {
            _tokenStore.AccessToken = string.Empty;

            _tokenStore.RefreshToken = string.Empty;
        }
        public class TokenResponse
        {
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;
        }
    }
}

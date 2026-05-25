using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using FinFlow.Modules.Auth.Auth.Model.Classes.Entities;
using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;
using FinFlow.Modules.Auth.Auth.Model.Classes.Responses;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using FinFlow.Modules.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinFlow.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBL _authBL;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthBL authBL, IPasswordService passwordService, IJwtService jwtService, IConfiguration configuration)
        {
            _authBL = authBL;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existingUser =
                await _authBL
                    .GetUserDetailsByUserName(request.Username);

            if (existingUser != null)
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        StatusCode = 400,

                        IsSuccess = false,

                        Message =
                            "Username already exists"
                    });
            }

            var (hash, salt) =
                _passwordService
                    .HashPassword(request.Password);

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),

                Username = request.Username,

                Email = request.Email,

                PasswordHash = hash,

                PasswordSalt = salt,

                IsActive = true
            };

            int result = await _authBL.CreateUser(user);
            if (result == 0)
            {
                return StatusCode(
                    500,
                    new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message =
                            "USer Registration failed"
                    });
            }
            return Ok(
                new ApiResponse<string>
                {
                    StatusCode = 200,

                    IsSuccess = true,

                    Message =
                        "User Registered Successfully"
                });
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _authBL.GetUserDetailsByUserName(loginRequest.Username);
            if (user == null)
            {
                return Unauthorized(
                     new ApiResponse<string>
                     {
                         StatusCode = 401,

                         IsSuccess = false,

                         Message = "Invalid Username",

                         Data = null
                     });
            }

            bool isPasswordValid = _passwordService.VerifyPassword(loginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if(isPasswordValid)
            {
                string accessToken = _jwtService.GenerateAccessToken(user);

                string refreshToken = _jwtService.GenerateRefreshToken();

                var refreshTokenEntity =
                    new RefreshTokenEntity
                    {
                        Id = Guid.NewGuid(),

                        CreatedAt = DateTime.UtcNow,

                        UserId = user.Id,

                        Token = refreshToken,

                        ExpiryDate =
                            DateTime.UtcNow.AddDays(7),

                        IsRevoked = false
                    };

                await _authBL.InsertRefreshToken(refreshTokenEntity);

                var authResponse =
                    new AuthResponse
                    {
                        AccessToken = accessToken,

                        RefreshToken = refreshToken
                    };

                return Ok(
                    new ApiResponse<AuthResponse>
                    {
                        StatusCode = 200,

                        IsSuccess = true,

                        Message = "Login Successful",

                        Data = authResponse
                    });
            }
            else
            {
                return Unauthorized(
                     new ApiResponse<string>
                     {
                         StatusCode = 401,

                         IsSuccess = false,

                         Message = "Invalid password",

                         Data = null
                     });
            }
            
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            RefreshTokenRequest request)
        {
            var existingToken =
                await _authBL
                    .GetRefreshTokenDetailsByToken(request.RefreshToken);

            if (existingToken == null)
            {
                return Unauthorized(
                    new ApiResponse<string>
                    {
                        StatusCode = 401,

                        IsSuccess = false,

                        Message =
                            "Invalid Refresh Token"
                    });
            }

            if (existingToken.IsRevoked)
            {
                return Unauthorized(
                    new ApiResponse<string>
                    {
                        StatusCode = 401,

                        IsSuccess = false,

                        Message =
                            "Refresh Token Revoked"
                    });
            }

            if (existingToken.ExpiryDate <= DateTime.UtcNow)
            {
                return Unauthorized(
                    new ApiResponse<string>
                    {
                        StatusCode = 401,

                        IsSuccess = false,

                        Message =
                            "Refresh Token Expired"
                    });
            }

            var user =
                await _authBL
                    .GetUserDetailsById(existingToken.UserId.ToString());

            if (user == null)
            {
                return Unauthorized(
                    new ApiResponse<string>
                    {
                        StatusCode = 401,

                        IsSuccess = false,

                        Message = "User Not Found"
                    });
            }

            // =========================================
            // ROTATE REFRESH TOKEN
            // =========================================

            // Revoke old token
            await _authBL.RevokeRefreshToken(existingToken.Token);

            // Create new refresh token
            string newRefreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity =
                new RefreshTokenEntity
                {
                    Id = Guid.NewGuid(),

                    UserId = user.Id,

                    Token = newRefreshToken,

                    OldRefreshToken = existingToken.Token,

                    CreatedAt = DateTime.UtcNow,

                    ExpiryDate =
                        DateTime.UtcNow.AddDays(
                            Convert.ToInt32(
                                _configuration["Jwt:RefreshTokenExpiryDays"])),

                    IsRevoked = false
                };

            await _authBL.UpdateRefreshToken(refreshTokenEntity);

            // Create new access token
            string newAccessToken = _jwtService.GenerateAccessToken(user);

            return Ok(
                new ApiResponse<AuthResponse>
                {
                    StatusCode = 200,

                    IsSuccess = true,

                    Message =
                        "Token Refreshed Successfully",

                    Data = new AuthResponse
                    {
                        AccessToken =
                            newAccessToken,

                        RefreshToken =
                            newRefreshToken
                    }
                });
        }

        [HttpGet("/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authBL.GetAllUserDetails();
            return Ok(
                new ApiResponse<List<IUserEntity>>
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message =
                        "Users Retrieved Successfully",
                    Data = users
                });
        }
    }

}

using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using FinFlow.Modules.Auth.Auth.Model.Classes.Entities;
using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;
using FinFlow.Modules.Auth.Auth.Model.Classes.Responses;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using FinFlow.Modules.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthBL authBL, IPasswordService passwordService, IJwtService jwtService, IEmailService emailService, IConfiguration configuration)
        {
            _authBL = authBL;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _emailService = emailService;
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

                IsActive = true,

                Role = "User",
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
                            "User Registration failed"
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
        [HttpPost("login")]
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

            if (isPasswordValid)
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
                    new LoginResponse
                    {
                        AccessToken = accessToken,

                        RefreshToken = refreshToken,
                        
                        UserData = user
                    };

                return Ok(
                    new ApiResponse<LoginResponse>
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
                new ApiResponse<LoginResponse>
                {
                    StatusCode = 200,

                    IsSuccess = true,

                    Message =
                        "Token Refreshed Successfully",

                    Data = new LoginResponse
                    {
                        AccessToken =
                            newAccessToken,

                        RefreshToken =
                            newRefreshToken
                    }
                });
        }

        [HttpGet("GetAllUsers")]
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

        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var username =
                User.FindFirst(
                    ClaimTypes.Name)?.Value;

            var role =
                User.FindFirst(
                    ClaimTypes.Role)?.Value;

            return Ok(
                new
                {
                    Username = username,
                    Role = role
                });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("assign_roles")]
        public async Task<IActionResult> AssignRoles(RolesRequest rolesRequest)
        {
            try
            {
                int result = await _authBL.AssignRoles(rolesRequest);
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
                           "Users Role Updated Successfully"
                   });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            // Validate Email
            if (string.IsNullOrWhiteSpace(
                request.Email))
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        StatusCode = 400,

                        IsSuccess = false,

                        Message = "Email is required"
                    });
            }

            // Check User
            var user = await _authBL.GetUserDetailsByEmail(request.Email);

            if (user == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        StatusCode = 404,

                        IsSuccess = false,

                        Message = "User not found"
                    });
            }

            // Generate OTP
            string otp =
                new Random()
                    .Next(100000, 999999)
                    .ToString();

            // Create OTP Entity
            var otpEntity = new OtpRequest
            {
                Id = Guid.NewGuid(),

                UserId = user.Id,

                Otp = otp,

                Purpose = "ForgotPassword",

                ExpiryDate =
                    DateTime.UtcNow.AddMinutes(5),

                IsUsed = false,

                CreatedAt =
                    DateTime.UtcNow
            };

            // Save OTP
            await _authBL.InsertOtp(otpEntity);

            // TODO:
            // Send OTP Email here
            bool result = await _emailService.SendOtpEmail(request.Email, otpEntity.Otp);

            if (result)
            {
                return Ok(
                    new ApiResponse<string>
                    {
                        StatusCode = 200,

                        IsSuccess = true,

                        Message =
                            $"OTP Sent Successfully : {otp}"
                    });
            }
            return StatusCode(
                    500,
                    new ApiResponse<string>
                    {
                        StatusCode = 500,
                        IsSuccess = false,
                        Message =
                            "OTP failed to Send"
                    });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            // Check User
            var user = await _authBL.GetUserDetailsByEmail(request.Email);

            if (user == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        StatusCode = 404,

                        IsSuccess = false,

                        Message = "User not found"
                    });
            }

            // Validate OTP
            var otpEntity = await _authBL.GetValidOtp(user.Id.ToString(), request.Otp, "ForgotPassword");

            if (otpEntity == null)
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        StatusCode = 400,

                        IsSuccess = false,

                        Message =
                            "Invalid or Expired OTP"
                    });
            }

            // Hash New Password
            var (hash, salt) =
                _passwordService
                    .HashPassword(
                        request.NewPassword);

            // Update Password
            await _authBL.UpdatePassword(user.Id.ToString(), hash, salt);

            // Mark OTP Used
            await _authBL.MarkOtpUsed(otpEntity.Id.ToString());

            // Revoke All Refresh Tokens
            await _authBL.RevokeAllUserTokens(user.Id.ToString());

            return Ok(
                new ApiResponse<string>
                {
                    StatusCode = 200,

                    IsSuccess = true,

                    Message =
                        "Password Reset Successful"
                });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            // Get Logged-In User Id
            string userId =
                User.FindFirst(
                    System.Security.Claims
                        .ClaimTypes
                        .NameIdentifier)?.Value;

            // Get User
            var user = await _authBL.GetUserDetailsById(userId);

            if (user == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        StatusCode = 404,

                        IsSuccess = false,

                        Message = "User not found"
                    });
            }

            // Verify Old Password
            bool isValidPassword = _passwordService.VerifyPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt);

            if (!isValidPassword)
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        StatusCode = 400,

                        IsSuccess = false,

                        Message = "Old Password is incorrect"
                    });
            }

            // Prevent Same Password
            if (request.OldPassword == request.NewPassword)
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        StatusCode = 400,

                        IsSuccess = false,

                        Message = "New password cannot be same as old password"
                    });
            }

            // Hash New Password
            var (hash, salt) = _passwordService.HashPassword(request.NewPassword);

            // Update Password
            await _authBL.UpdatePassword( user.Id.ToString(), hash, salt);

            // Revoke All Refresh Tokens
            // User must login again
            await _authBL.RevokeAllUserTokens(user.Id.ToString());

            return Ok(
                new ApiResponse<string>
                {
                    StatusCode = 200,

                    IsSuccess = true,

                    Message = "Password changed successfully"
                });
        }
    }
}

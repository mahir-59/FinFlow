using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.DL.Interfaces
{
    public interface IAuthDL
    {
        Task<List<IUserEntity>> GetAllUserDetails();
        Task<IUserEntity> GetUserDetailsByUserName(string username);
        Task<IUserEntity> GetUserDetailsByEmail(string email);
        Task<IUserEntity> GetUserDetailsById(string id);
        Task<int> CreateUser(IUserEntity userEntity);
        Task<IRefreshTokenEntity> GetRefreshTokenDetailsByToken(string refreshToken);
        Task<int> InsertRefreshToken(IRefreshTokenEntity refreshTokenEntity);
        Task<int> UpdateRefreshToken(IRefreshTokenEntity refreshTokenEntity);
        Task<int> RevokeRefreshToken(string refreshToken);
        Task<int> AssignRoles(IRolesRequest rolesRequest);
        Task<int> InsertOtp(IOtpRequest otpRequest);
        Task<IOtpRequest> GetValidOtp(string userId, string otp, string purpose);
        Task<int> UpdatePassword(string userId, string hash, string salt);
        Task<int> MarkOtpUsed(string id);
        Task<int> RevokeAllUserTokens(string userId);
    }
}

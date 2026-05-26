using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using FinFlow.Modules.Auth.Auth.DL.Interfaces;
using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.BL.Classes
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDL _authDL;
        public AuthBL(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        public async Task<List<IUserEntity>> GetAllUserDetails()
        {
            return await _authDL.GetAllUserDetails();
        }

        public async Task<IUserEntity> GetUserDetailsById(string id)
        {
            return await _authDL.GetUserDetailsById(id);
        }

        public async Task<IUserEntity> GetUserDetailsByEmail(string email)
        {
            return await _authDL.GetUserDetailsByEmail(email);
        }

        public async Task<IUserEntity> GetUserDetailsByUserName(string username)
        {
            return await _authDL.GetUserDetailsByUserName(username);
        }

        public async Task<int> CreateUser(IUserEntity userEntity)
        {
            return await _authDL.CreateUser(userEntity);
        }

        public async Task<IRefreshTokenEntity> GetRefreshTokenDetailsByToken(string refreshToken)
        {
            return await _authDL.GetRefreshTokenDetailsByToken(refreshToken);
        }

        public async Task<int> InsertRefreshToken(IRefreshTokenEntity refreshTokenEntity)
        {
            return await _authDL.InsertRefreshToken(refreshTokenEntity);
        }

        public async Task<int> RevokeRefreshToken(string refreshToken)
        {
            return await _authDL.RevokeRefreshToken(refreshToken);
        }

        public async Task<int> UpdateRefreshToken(IRefreshTokenEntity refreshTokenEntity)
        {
            return await _authDL.UpdateRefreshToken(refreshTokenEntity);
        }

        public async Task<int> AssignRoles(IRolesRequest rolesRequest)
        {
            return await _authDL.AssignRoles(rolesRequest);
        }

        public async Task<int> InsertOtp(IOtpRequest otpRequest)
        {
            return await _authDL.InsertOtp(otpRequest);
        }

        public async Task<IOtpRequest> GetValidOtp(string userId, string otp, string purpose)
        {
            return await _authDL.GetValidOtp(userId, otp, purpose);
        }

        public async Task<int> UpdatePassword(string userId, string hash, string salt)
        {
            return await _authDL.UpdatePassword(userId, hash, salt);
        }
        
        public async Task<int> MarkOtpUsed(string id)
        {
            return await _authDL.MarkOtpUsed(id);
        }

        public async Task<int> RevokeAllUserTokens(string userId)
        {
            return await _authDL.RevokeAllUserTokens(userId);
        }
    }
}

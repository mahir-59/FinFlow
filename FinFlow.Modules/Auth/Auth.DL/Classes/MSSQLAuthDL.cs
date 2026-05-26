using FinFlow.Modules.Auth.Auth.DL.Interfaces;
using FinFlow.Modules.Auth.Auth.Model.Classes.Entities;
using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using FinFlow.Modules.Common.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.DL.Classes
{
    public class MSSQLAuthDL : IAuthDL
    {
        private readonly SqlHelper _sqlHelper;
        public MSSQLAuthDL(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public async Task<IUserEntity> GetUserDetailsById(string id)
        {
            try
            {
                var sql = "Select * from Users where Id = @Id";

                return await _sqlHelper.QueryFirstOrDefaultAsync<UserEntity?>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IUserEntity> GetUserDetailsByUserName(string username)
        {
            try
            {
                var sql = "Select * from Users where username = @UserName";

                return await _sqlHelper.QueryFirstOrDefaultAsync<UserEntity?>(sql, new { UserName = username });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IUserEntity> GetUserDetailsByEmail(string email)
        {
            try
            {
                var sql = "Select * from Users where email = @Email";

                return await _sqlHelper.QueryFirstOrDefaultAsync<UserEntity?>(sql, new { Email = email });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> CreateUser(IUserEntity userEntity)
        {
            try
            {
                var sql = @"INSERT INTO Users
                            (
                                Id,
                                Username,
                                Email,
                                PasswordHash,
                                PasswordSalt,
                                IsActive,
                                Role
                            )
                            VALUES
                            (
                                @Id,
                                @Username,
                                @Email,
                                @PasswordHash,
                                @PasswordSalt,
                                @IsActive,
                                @Role
                            )";
                return await _sqlHelper.ExecuteAsync(sql, userEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IRefreshTokenEntity> GetRefreshTokenDetailsByToken(string refreshToken)
        {
            try
            {
                var sql = @"Select * from RefreshTokens where token = @RefreshToken";
                return await _sqlHelper.QueryFirstOrDefaultAsync<RefreshTokenEntity?>(sql, new { RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertRefreshToken(IRefreshTokenEntity refreshTokenEntity)
        {
            try
            {
                var sql = @"Insert into RefreshTokens(Id, UserId, Token, ExpiryDate, CreatedAt, IsRevoked, CreatedByIp)
                            Values(@Id, @UserId, @Token, @ExpiryDate, @CreatedAt, @IsRevoked, @CreatedByIp)";
                return await _sqlHelper.ExecuteAsync(sql, refreshTokenEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> RevokeRefreshToken(string refreshToken)
        {
            try
            {
                var sql = $@"Update RefreshTokens set IsRevoked = 1 , RevokedAt = '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}' where Token = '{refreshToken}'";
                return await _sqlHelper.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateRefreshToken(IRefreshTokenEntity refreshTokenEntity)
        {
            try
            {
                var sql = $@"Update RefreshTokens set ReplacedByToken = @Token where UserId = @UserId And Token = @OldRefreshToken";
                int result = await _sqlHelper.ExecuteAsync(sql, refreshTokenEntity);
                if (result == 0)
                {
                    return -1;
                }
                else
                {
                    return await InsertRefreshToken(refreshTokenEntity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<IUserEntity>> GetAllUserDetails()
        {
            try
            {
                var sql = "Select * from Users";
                var users = (await _sqlHelper.QueryAsync<UserEntity>(sql)).ToList();
                return users.Cast<IUserEntity>().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> AssignRoles(IRolesRequest rolesRequest)
        {
            try
            {
                var sql = @"Update Users Set Role = @RoleName Where Id = @UserId";
                return await _sqlHelper.ExecuteAsync(sql, rolesRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertOtp(IOtpRequest otpRequest)
        {
            try
            {
                var sql = @"INSERT INTO Otps
                            (
                                Id,
                                UserId,
                                Otp,
                                Purpose,
                                ExpiryDate,
                                IsUsed,
                                CreatedAt
                            )
                            VALUES
                            (
                                @Id,
                                @UserId,
                                @Otp,
                                @Purpose,
                                @ExpiryDate,
                                @IsUsed,
                                @CreatedAt
                            )";
                return await _sqlHelper.ExecuteAsync(sql, otpRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IOtpRequest> GetValidOtp(string userId, string otp, string purpose)
        {
            try
            {
                var sql = @"Select * from Otps where UserId = @UserId And Otp = @Otp And ExpiryDate > GETUTCDATE() And IsUsed = 0";
                return await _sqlHelper.QueryFirstOrDefaultAsync<OtpRequest?>(sql, new { UserId = userId, Otp = otp });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdatePassword(string userId, string hash, string salt)
        {
            try
            {
                var sql = @"Update Users Set PasswordHash = @Hash, PasswordSalt = @Salt Where Id = @UserId";
                return await _sqlHelper.ExecuteAsync(sql, new { Hash = hash, Salt = salt, UserId = userId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> MarkOtpUsed(string id)
        {
            try
            {
                var sql = @"Update Otps Set IsUsed = 1 Where Id = @Id";
                return await _sqlHelper.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> RevokeAllUserTokens(string userId)
        {
            try
            {
                var sql = $@"Update RefreshTokens set IsRevoked = 1 , RevokedAt = '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}' where UserId = '{userId}'";
                return await _sqlHelper.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

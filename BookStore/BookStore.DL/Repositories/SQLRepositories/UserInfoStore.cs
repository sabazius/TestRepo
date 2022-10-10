using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.SQLRepositories
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo> //: IUserInfoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserInfoStore> _logger;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public UserInfoStore(IConfiguration configuration,
            ILogger<UserInfoStore> logger,
            IPasswordHasher<UserInfo> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var user = await conn.QueryAsync<UserInfo>("SELECT * FROM UserInfo WHERE Email = @email AND Password =  @password", new { Email = email, Password = password });

                    return user.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in UserInfoStore.GetById {e.Message}", e);
                }
            }

            return null;
        }

        public void Dispose()
        {
        }

        public async Task<string?> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync(cancellationToken);

                var result = await conn.QuerySingleOrDefaultAsync<UserInfo>(
                    "SELECT * FROM UserInfo WHERE UserId = @UserId",
                    new { UserId = user.UserId });

                return result?.UserId.ToString();
            }
        }

        public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    string query =
                        @"INSERT INTO UserInfo 
                        ([DisplayName]
                       ,[UserName]
                       ,[Email]
                       ,[Password]
                       ,[CreatedDate]) VALUES(
                        @DisplayName
                       ,@UserName
                       ,@Email
                       ,@Password
                       ,@CreatedDate)";

                    user.Password = _passwordHasher.HashPassword(user, user.Password);

                    var result = await conn.ExecuteAsync(query, user);

                    return result > 0 ? IdentityResult.Success : IdentityResult.Failed();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in UserInfoStore.Add {e.Message}", e);

                    return IdentityResult.Failed(new IdentityError()
                    {
                        Description = e.Message
                    });
                }
            }
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo?> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    IEnumerable<UserInfo?> result = await conn.QueryAsync<UserInfo>(
                        "SELECT * FROM UserInfo WHERE UserName = @userName",
                        new { UserName = userName });

                    return result?.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
              _logger.LogError($"Error in {nameof(UserInfoStore.FindByNameAsync)}", e);
              return null;
            }
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn =
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @passwordHash WHERE UserId = @userId",
                new {user.UserId, passwordHash});
        }

        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn =
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>(
                "SELECT Password FROM UserInfo WITH(NOLOCK) WHERE UserId = @UserId", new {user.UserId});
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var result = 
                        await conn.QueryAsync<string>("SELECT r.RoleName FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId )", new { UserId = user.UserId});

                    return result.ToList();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error in {nameof(UserRoleStore.FindByNameAsync)}:{e.Message}");
                    return null;
                }
            }
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

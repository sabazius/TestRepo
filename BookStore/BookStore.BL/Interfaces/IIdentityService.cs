using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(UserInfo user);

        Task<UserInfo?> CheckUserAndPass(string userName, string password);

        Task<IEnumerable<string>> GetUserRoles(UserInfo user);
    }
}

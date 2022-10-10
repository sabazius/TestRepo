using BookStore.Models.Models.Users;

namespace BookStore.BL.Interfaces
{
    public interface IUserInfoService
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}

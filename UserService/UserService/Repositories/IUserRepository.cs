using UserService.Entities;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUserByRefreshToken(string refreshToken);
    }
}

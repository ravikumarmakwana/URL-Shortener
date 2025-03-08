using Microsoft.EntityFrameworkCore;
using UserService.Contexts;
using UserService.Entities;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUserByRefreshToken(string refreshToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.RefreshTokenExpiryTime >= DateTime.Now);
        }
    }
}

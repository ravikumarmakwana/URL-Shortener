using System.Security.Claims;
using URLService.Models;

namespace URLService.Core
{
    public static class Helper
    {
        public static UserClaims GetUserClaims(this HttpContext context)
        {
            return new UserClaims
            {
                UserId = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")
            };
        }
    }
}

using BookStoresWebAPI.Entities;

namespace BookStoresWebAPI.Jwt.Handler
{
    public interface IJwtTokenHandler
    {
        string GenerateAccessToken(int userId);
        RefreshToken GenerateRefreshToken();
        bool ValidateRefreshToken(User user, string refreshToken);
        Task<User> GetUserFromAccessToken(string accessToken);
        bool UserExists(int id);

    }
}

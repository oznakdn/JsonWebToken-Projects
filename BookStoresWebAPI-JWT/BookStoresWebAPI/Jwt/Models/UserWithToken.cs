using BookStoresWebAPI.Entities;

namespace BookStoresWebAPI.Jwt.Models
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            UserId = user.UserId;
            EmailAddress = user.EmailAddress;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            PubId = user.PubId;
            HireDate = user.HireDate;
            Role = user.Role;
        }

    }
}

using BookStoresWebAPI.Data;
using BookStoresWebAPI.Dtos;
using BookStoresWebAPI.Entities;
using BookStoresWebAPI.Jwt.Handler;
using BookStoresWebAPI.Jwt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoresWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BookStoresDBContext _dbContext;
        private readonly IJwtTokenHandler _tokenHandler;


        public UsersController(BookStoresDBContext dbContext, IJwtTokenHandler tokenHandler)
        {
            _dbContext = dbContext;
            _tokenHandler = tokenHandler;
        }

        #region GetUserRole
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserRole(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.UserId == id);


            // One to One Relational Data
            await _dbContext.Entry(user)
                .Reference(user => user.Role)
                .LoadAsync();

            return Ok(user.Role.RoleDesc);
        }
        #endregion

        #region GetUsers
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        #endregion

        #region GetUser
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            string emailAddress = HttpContext.User.Identity.Name;
            var user = await _dbContext.Users.Where(x => x.EmailAddress == emailAddress).FirstOrDefaultAsync();

            user.Password = null;

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        #endregion

        #region GetUserById
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        #endregion

        #region GetUserDetails
        [HttpGet("GetUserDetails/{id}")]
        public async Task<ActionResult<User>> GetUserDetails(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Role)
                                            .Where(u => u.UserId == id)
                                            .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        {
            user = await _dbContext.Users.Include(u => u.Role)
                                        .Where(u => u.EmailAddress == user.EmailAddress
                                                && u.Password == user.Password).FirstOrDefaultAsync();

            UserWithToken userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = _tokenHandler.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _dbContext.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return NotFound();
            }

            //sign your token here here..
            userWithToken.AccessToken = _tokenHandler.GenerateAccessToken(user.UserId);
            return userWithToken;
        }
        #endregion

        #region Register
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserWithToken>> RegisterUser([FromBody] User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            //load role for registered user
            user = await _dbContext.Users.Include(u => u.Role)
                                        .Where(u => u.UserId == user.UserId).FirstOrDefaultAsync();

            UserWithToken userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = _tokenHandler.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _dbContext.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return NotFound();
            }

            //sign your token here here..
            userWithToken.AccessToken = _tokenHandler.GenerateAccessToken(user.UserId);
            return userWithToken;
        }
        #endregion

        #region RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            User user = await _tokenHandler.GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && _tokenHandler.ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = _tokenHandler.GenerateAccessToken(user.UserId);

                return userWithToken;
            }

            return null;
        }
        #endregion

        #region GetUserByAccessToken
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<User>> GetUserByAccessToken([FromBody] string accessToken)
        {
            User user = await _tokenHandler.GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }
        #endregion

        #region CreateUser
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }
        #endregion

        #region UpdateUser
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_tokenHandler.UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        #endregion

        #region DeleteUser
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
        #endregion
    }
}

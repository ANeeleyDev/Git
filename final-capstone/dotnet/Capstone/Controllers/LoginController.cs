using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;

        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            userDao = _userDao;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = Unauthorized(new { message = "Username or password is incorrect" });

            // Get the user by username
            User user = userDao.GetUser(userParam.Username);

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.UserId, user.Username, user.Role);

                // Create a ReturnUser object to return to the client
                LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { UserId = user.UserId, Username = user.Username, Role = user.Role }, Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
            }

            return result;
        }


        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterUser userParam)
        {
            IActionResult result;

            User existingUser = userDao.GetUser(userParam.Username);
            if (existingUser != null)
            {
                return Conflict(new { message = "Username already taken. Please choose a different username." });
            }

            User user = userDao.AddUser(userParam.Username, userParam.Password, userParam.Role, userParam.FirstName, userParam.LastName, userParam.EmailAddress, userParam.PhoneNumber, userParam.StreetAddress, userParam.City, userParam.State, userParam.Zip);
            if (user != null)
            {
                result = Created(user.Username, null); //values aren't read on client
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and user was not created." });
            }

            return result;
        }


        //Misc methods

        [HttpGet("/user/{userId}")]
        [AllowAnonymous]
        public User DisplayUser(int userId) //Display user details
        {
            return userDao.DisplayUser(userId);
        }



        //Registered user methods

        [HttpGet("/myprofile")]
        [Authorize]
        public User DisplayLoggedInUser(int userId) //View their profile
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return userDao.DisplayUser(userId);
        }

        [HttpDelete("/myprofile")]
        [Authorize]
        public bool DeleteLoggedInUser(int userId) //Delete their profile
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return userDao.DeleteUser(userId);
        }

        [HttpPut("/myprofile")]
        [Authorize]
        public bool UpdateLoggedInUser(User updatedUser, int userId) //Update their profile
        {
            string userIdString = User.FindFirst("sub")?.Value;
            userId = Convert.ToInt32(userIdString);

            return userDao.UpdateUser(updatedUser, userId);
        }



        //Admin methods

        [HttpDelete("/admin/{userId}")]
        [Authorize(Roles = "admin")]
        public bool DeleteUser(int userId) //Delete any profile
        {
            return userDao.DeleteUser(userId);
        }

        [HttpPut("/admin/{userId}")]
        [Authorize(Roles = "admin")]
        public bool UpdateUser(User updatedUser, int userId) //Can change any user's profile
        {
            return userDao.UpdateUser(updatedUser, userId);
        }

        [HttpPut("/admin/{userId}/updaterole")]
        [Authorize(Roles = "admin")]
        public bool UpdateUserRole(User updatedUser, int userId) //Can change user's role to user, mod, or admin
        {
            return userDao.UpdateUserRole(updatedUser, userId);
        }
    }
}

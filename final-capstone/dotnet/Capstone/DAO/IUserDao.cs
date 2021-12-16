using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        //Misc methods
        User GetUser(string username);
        User AddUser(string username, string password, string role, string firstName, string lastName, string emailAddress, 
            string phoneNumber, string streetAddress, int city, int state, int zip);
        public List<City> GetAllCities();

        public DisplayUser DisplayUser(int userId);
        public bool DeleteUser(int userId); //Registered users can delete their profile
        public bool UpdateUser(User updatedUser, int userId); //admins can change a user's profile
        public bool UpdateUserRole(User updatedUser, int userId); //admins can change a user's role
    }
}

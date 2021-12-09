using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role, string firstName, string lastName, string emailAddress, 
            string phoneNumber, string streetAddress, int city, int state, int zip);



        //Admin methods
        public bool UpdateUserRole(User updatedUser, int userId); //admins can change a user's role
    }
}

using Blog.Model.Entities;
using System.Collections.Generic;

namespace Blog.Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);

        void DeleteUser(string id);

        User GetUser(string email);

        IEnumerable<User> GetUsers();

        bool IsEmailUnique(string email);

        bool IsUsernameUnique(string username);
    }
}
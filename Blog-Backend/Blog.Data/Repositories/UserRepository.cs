using Blog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public virtual User GetUser(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public virtual void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public virtual void DeleteUser(string id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public virtual bool IsEmailUnique(string email)
        {
            var user = _context.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public virtual bool IsUsernameUnique(string username)
        {
            var user = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
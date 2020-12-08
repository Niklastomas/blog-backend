using Blog.Data.DTO;
using Blog.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.OrderByDescending(x => x.Published).ToList();
        }

        public PostDTO GetPost(string id)
        {
            return _context.Posts.
                Where(x => x.Id == id)
                .Include(x => x.User)
                .Select(x => new PostDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Published = x.Published,
                    Image = x.Image,
                    User = new UserDTO()
                    {
                        Id = x.User.Id,
                        Username = x.User.Username,
                        Email = x.User.Email
                    }
                })
                .FirstOrDefault();
        }

        public bool CreatePost(Post post)
        {
            if (post != null)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePost(string id)
        {
            var post = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Post> GetUserPosts(string userId)
        {
            return _context.Posts.Where(x => x.UserId == userId).ToList();
        }
    }
}
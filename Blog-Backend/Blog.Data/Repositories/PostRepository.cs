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
            return _context.Posts.ToList();
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
                    User = new UserDTO()
                    {
                        Id = x.User.Id,
                        Username = x.User.Username,
                        Email = x.User.Email
                    }
                })
                .FirstOrDefault();
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(string id)
        {
            var post = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
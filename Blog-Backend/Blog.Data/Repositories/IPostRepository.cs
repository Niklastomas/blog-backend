using Blog.Data.DTO;
using Blog.Model.Entities;
using System.Collections.Generic;

namespace Blog.Data.Repositories
{
    public interface IPostRepository
    {
        bool CreatePost(Post post);

        bool DeletePost(string id);

        PostDTO GetPost(string id);

        List<Post> GetPosts();

        List<Post> GetUserPosts(string userId);
    }
}
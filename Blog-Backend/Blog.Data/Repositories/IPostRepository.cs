using Blog.Data.DTO;
using Blog.Model.Entities;
using System.Collections.Generic;

namespace Blog.Data.Repositories
{
    public interface IPostRepository
    {
        void CreatePost(Post post);

        void DeletePost(string id);

        PostDTO GetPost(string id);

        List<Post> GetPosts();
    }
}
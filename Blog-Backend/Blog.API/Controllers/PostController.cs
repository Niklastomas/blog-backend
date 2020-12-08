using Blog.API.ViewModels;
using Blog.Data.Repositories;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/<PostController>
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            var posts = _postRepository.GetPosts();
            if (posts == null)
            {
                return BadRequest(new { message = "No posts found" });
            }
            return Ok(posts);
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var post = _postRepository.GetPost(id);
            if (post == null)
            {
                return BadRequest(new { message = "No post found" });
            }
            return Ok(post);
        }

        // GET api/<PostController>/5
        [HttpGet("[action]/{userId}")]
        public ActionResult<List<Post>> GetUserPosts(string userId)
        {
            var posts = _postRepository.GetUserPosts(userId);
            if (posts == null)
            {
                return BadRequest(new { message = "No post found" });
            }
            return Ok(posts);
        }

        // POST api/<PostController>
        [HttpPost]
        public ActionResult<Post> Post([FromBody] PostViewModel postVM)
        {
            var id = Guid.NewGuid().ToString();
            var post = new Post()
            {
                Id = id,
                Title = postVM.Title,
                Content = postVM.Content,
                Published = DateTime.Now,
                Image = postVM.Image,
                UserId = postVM.UserId
            };

            var postCreated = _postRepository.CreatePost(post);
            if (postCreated == true)
            {
                return Ok(post);
            }
            return BadRequest(new { message = "Failed to save post" });
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            var postDeleted = _postRepository.DeletePost(id);
            if (postDeleted == true)
            {
                return Ok(id);
            }
            return BadRequest(new { message = "Failed to delete post" });
        }
    }
}
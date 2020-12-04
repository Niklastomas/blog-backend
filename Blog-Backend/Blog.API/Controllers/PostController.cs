using Blog.API.ViewModels;
using Blog.Data.Repositories;
using Blog.Model.Entities;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] PostViewModel postVM)
        {
            var id = Guid.NewGuid().ToString();
            var post = new Post()
            {
                Id = id,
                Title = postVM.Title,
                Content = postVM.Content,
                Published = DateTime.Now,
                UserId = postVM.UserId
            };

            _postRepository.CreatePost(post);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
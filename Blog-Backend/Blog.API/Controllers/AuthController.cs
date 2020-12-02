using Blog.API.Services;
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
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        // POST api/<AuthController>/login
        [HttpPost("login")]
        public ActionResult<AuthData> Post([FromBody] LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(loginVM.Email);
            if (user == null)
            {
                return BadRequest(new { email = "No user with this email" });
            }

            var passwordValid = _authService.VerifyPassword(loginVM.Password, user.Password);
            if (!passwordValid)
            {
                return BadRequest(new { password = "invalid password" });
            }

            return _authService.GetAuthData(user.Id);
        }

        [HttpPost("register")]
        public ActionResult<AuthData> Post([FromBody] RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailUnique = _userRepository.IsEmailUnique(registerVM.Email);
            if (!emailUnique)
            {
                return BadRequest(new { email = "user with this email already exists" });
            }

            var usernameUnique = _userRepository.IsUsernameUnique(registerVM.Username);
            if (!usernameUnique)
            {
                return BadRequest(new { username = "user with this username already exists" });
            }

            var id = Guid.NewGuid().ToString();
            var newUser = new User()
            {
                Id = id,
                Username = registerVM.Username,
                Email = registerVM.Email,
                Password = _authService.HashPassword(registerVM.Password)
            };

            _userRepository.AddUser(newUser);

            return _authService.GetAuthData(id);
        }
    }
}
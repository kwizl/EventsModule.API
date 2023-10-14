using AutoMapper;
using EventsModule.API.Interfaces;
using EventsModule.Data.Models;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsModule.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBearerToken _tokenService;
        private IMapper _mapper;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager,
            IBearerToken tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        // Register User
        [HttpPost("Register")]
        public async Task<ActionResult<Token>> Register(Authenticate register)
        {
            // Checks if user is already registered
            if (await UserExists(register.UserName)) return BadRequest("Username is taken");

            var user = _mapper.Map<User>(register);

            user.UserName = user.UserName.ToLower();
            user.DateCreated = DateTime.UtcNow.AddHours(3.00);
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new Token
            {
                Value = _tokenService.CreateToken(user)
            };
        }

        // Login User
        [HttpPost("Login")]
        public async Task<ActionResult<Token>> Login(Authenticate login)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == login.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid Name or Password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new Token
            {
                Value = _tokenService.CreateToken(user)
            };
        }

        // Check if user has already registered
        private async Task<bool> UserExists(string userName)
        {
            return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SubwayEntrance.Data;
using SubwayEntrance.Data.Helpers;
using SubwayEntrance.Data.JWTHelper;
using SubwayEntrance.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SubwayEntrance.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthRepository<User> userRepo;
        private readonly IConfiguration conf;

        public UsersController(IAuthRepository<User> _userRepo, IConfiguration config)
        {
            userRepo = _userRepo;
            conf = config;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LogIn model)
        {
            var user = await userRepo.LogIn(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            string secret = this.conf.GetValue<string>("Secret");
            var jwtHelper = new JWTHelper(secret);
            var token = jwtHelper.CreateToken(model.Email);

            return Ok(new
            {
                ok = true,
                msg = "Log-In success",
                token
            });
        }



        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Register model)
        {
            // map model to entity

            var auth = new User
            {
                 FirstName = model.FirstName,
                  Email = model.Email,
            };
            try
            {
                // create user
                userRepo.Register(auth, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

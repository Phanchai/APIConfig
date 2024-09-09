using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Class;
using APIConfig.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIConfig.Controllers
{
 
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        TokenSet tokenSet1 = new TokenSet();
        private readonly TokenService _tokenService;
        private readonly UserReponitory _userReponitory;
        public AuthController(TokenService tokenService, UserReponitory userReponitory)
        {
            _tokenService = tokenService;
            _userReponitory = userReponitory;
        }

        [HttpPost("auth/login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userReponitory.GetUserManager(request.Username);

            if (user == null && PasswordHelper.VerifyPassword(user.PasswordHash, request.Password))
            {
                Thread.Sleep(200000);
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }
    }
}
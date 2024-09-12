using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Class;
using APIConfig.Models;
using APIConfig.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Service;

namespace APIConfig.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly ProjectManager2Context _context;
        private PasswordHelper EncodePassword;
        private readonly LogService _logController;
        public AuthController(TokenService tokenService,ProjectManager2Context context,LogService logController)
        {
            _tokenService = tokenService;
            _logController = logController;
            _context = context;
        }

        [HttpGet("start")]
        public async Task<IActionResult> Get([FromHeader] string ipAddress,[FromHeader] string username = null, [FromHeader] string password = null)
        {
            var userAccess = await _context.UserManagers.FirstOrDefaultAsync(user => user.ipAddress == ipAddress);
            try
            {
               
                 if(userAccess == null)
                 {
                        if (username == null || password == null)
                        {
                            //await _logController.SendLog("Unauthorized access attempt","Get : Start");
                            return Unauthorized("Username and Password are Required");
                        }

                        var hashedPassword = EncodePassword.HashPassword(password);
                        _context.UserManagers.Add(new UserManager{
                            ipAddress = ipAddress,
                            Username = username,
                        });
                        await _context.SaveChangesAsync();
                       
                        return Ok("User access granted.");
                 }

                 var token = _tokenService.GenerateToken(userAccess.Username);
                 await _logController.SendLog($"IPAddress : {userAccess.ipAddress}","Get : Start","เข้าสู่ระบบ");
                 return Ok(new {
                    Username = userAccess.Username,
                    FirstName = userAccess.FirstName,
                    MidleName = userAccess.MidleName,
                    LastName = userAccess.LastName,
                    Token = token
                 });

            }
            catch(Exception ex)
            {
                await _logController.SendLog($"IPAddress : {userAccess.ipAddress}","Start : Get", ex.ToString());
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
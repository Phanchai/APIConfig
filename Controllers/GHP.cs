using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Class;
using APIConfig.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIConfig.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GHP : ControllerBase
    {
        private readonly ProjectManager2Context _context;

        public GHP(ProjectManager2Context context)
        {
            _context = context;
        }

        PasswordHelper passwordHelper = new PasswordHelper();

        [HttpGet("select/usermanager")]
        public async Task<IActionResult> Get(){
            var user = await _context.UserManagers.ToListAsync();
            return Ok(user);
        }

        [HttpGet("select/usermanager/{username}")]
        public async Task<ActionResult<object>> GetOnUsername(string username){
            var user = await _context.UserManagers.SingleOrDefaultAsync(a => a.Username == username);
            return Ok(user);
        }


        [HttpPost("insert/usermanager")]
        public async Task<IActionResult> Insert([FromBody] UserManager userManager){
            try{
                        string passwordH = PasswordHelper.HashPassword(userManager.Password);

                        var _userManager = new UserManager
                        {
                            Uid = userManager.Uid,
                            Email = userManager.Email,
                            Username = userManager.Username,
                            Password = userManager.Password,
                            FirstName = userManager.FirstName,
                            LastName = userManager.LastName,
                            PasswordHash = passwordH,
                            PasswordSalt = userManager.PasswordSalt,
                        };
                _context.Add(_userManager);
                await _context.SaveChangesAsync();
                return Ok(_userManager);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}
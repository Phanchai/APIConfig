using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace APIConfig.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Log : ControllerBase
    {
        private readonly IHubContext<LogHub> _hubContext;

        public Log(IHubContext<LogHub> hubContext){
            _hubContext = hubContext;
        }

        [HttpGet("sendlog")]
        public async Task<IActionResult> SendLog(string message,string pages){
            await _hubContext.Clients.All.SendAsync("ReceiveLog",message,pages);
            return Ok("Success : ");
        }
    }
}
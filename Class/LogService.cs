using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Class;
using Microsoft.AspNetCore.SignalR;

namespace Service
{
    public class LogService
    {
        private readonly IHubContext<LogHub> _hubContext;

        public LogService(IHubContext<LogHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendLog(string ip,string page,string message)
        {
            try
            {
               await _hubContext.Clients.All.SendAsync("ReceiveLog", ip, page,message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
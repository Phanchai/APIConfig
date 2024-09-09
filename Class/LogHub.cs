using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace APIConfig.Class
{
    public class LogHub : Hub
    {
        public async Task SendLog(string logMessage)
        {
            await Clients.All.SendAsync("ReceiveLog",logMessage);    
        }
    }
}
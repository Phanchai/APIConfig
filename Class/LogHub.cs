using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.SignalR;

namespace APIConfig.Class
{
    public class LogHub : Hub
    {
        public async Task SendLog(string logMessage,string page)
        {
            await Clients.All.SendAsync("ReceiveLog",logMessage,page);    
        }
    }
}
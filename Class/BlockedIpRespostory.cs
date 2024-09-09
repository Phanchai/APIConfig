using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Interface;
using APIConfig.Models;
using Microsoft.EntityFrameworkCore;

namespace APIConfig.Class
{
    public class BlockedIpRespostory : IBlockedIpRepository
    {
        private readonly ProjectManager2Context _context;

        public BlockedIpRespostory(ProjectManager2Context context)
        {
            _context = context;
        }

        public async Task AddBlockedIpAsync(string ipAddress, string reason){

            var blockedIp = new BlockedIps{
                IpAddress = ipAddress,
                BlockedAt = DateTime.UtcNow,
                Reason = reason
            };

            _context.BlockedIps.Add(blockedIp);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBlockedAsync(string ipAddress)
        {
            return await _context.BlockedIps.AnyAsync(b => b.IpAddress == ipAddress);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConfig.Interface
{
    public interface IBlockedIpRepository
    {
           Task AddBlockedIpAsync(string ipAddress, string reason);
            Task<bool> IsBlockedAsync(string ipAddress);
    }
}
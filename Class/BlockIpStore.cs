using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConfig.Class
{
    public static class BlockIpStore
    {
        private static readonly HashSet<string> _blockIpStoreNames = new HashSet<string>();

        public static void Add(string ip){
            _blockIpStoreNames.Add(ip);
        }

        public static bool IsBlocked(string ip){
            return _blockIpStoreNames.Contains(ip);
        }
    }
}
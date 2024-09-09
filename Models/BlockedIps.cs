using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConfig.Models
{
    public class BlockedIps
    {
        public int Id { get; set; }
        public string ?IpAddress { get; set; }
        public DateTime BlockedAt { get; set; }
        public string Reason { get; set; } = "";
    }
}
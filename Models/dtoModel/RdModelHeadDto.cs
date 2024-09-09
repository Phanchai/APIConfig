using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConfig.Models
{
    public class RdModelHeadDto
    {
        public string ProjectName { get; set; } = null!;

        public int CodeProject { get; set; }

        public string? ProjectOwner { get; set; }

        public string? Deskip { get; set; }

        public DateTime? DateGet { get; set; }

        public DateTime? DateFinal { get; set; }

        public string? InitialsBrand { get; set; }

        public int BrandNumber { get; set; }

        public string? NameBrand { get; set; }

        public int CapId { get; set; }

        public string? CapCode { get; set; }

        public string? CapName { get; set; }

        public string? GroupCap { get; set; }

        public int? CapStandard { get; set; }

        public int? CapCopy { get; set; }

        public string? TeamName { get; set; }

        public int TeamCode { get; set; }

        public string? DraftmanName { get; set; }

        public int DraftmanCode { get; set; }

        public string? BossName { get; set; }

        public int? BossId { get; set; }

        public string? MonthName { get; set; }

        public int MonthCount { get; set; }

        public int? CapInMonth { get; set; }

    }
}
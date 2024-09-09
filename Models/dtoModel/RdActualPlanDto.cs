using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConfig.Models.dtoModel
{
    public class RdActualPlanDto
    {
        public int Article { get; set; }

        public int? CodeProject { get; set; }

        public string? ProjectName { get; set; }

        public string? InitialsBrand { get; set; }

        public string? TypeNames { get; set; }

        public string? Remark { get; set; }

        public string? DraftmanName { get; set; }

        public int? DraftmanCode { get; set; }

        public string? TeamName { get; set; }

        public int? TeamCode { get; set; }

        public int? JobNot { get; set; }

        public int? JobFinish { get; set; }

        public string? CapName { get; set; }

        public string? CapType { get; set; }

        public int? CapQty { get; set; }

        public int? CapHour { get; set; }
        public int? CapCopy { get; set; }

        public int? CapStandard { get; set; }

        public int? MonthCount { get; set; }

        public string? OptionsSelect { get; set; }

        public DateTime FirstDate { get; set; }
        
        public DateTime FinishDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ApproveDate { get; set; }
        public DateTime DtimeSave {get; set; }
        public DateTime DateSave {get; set; }
        
        public string? DateTimeRec { get; set; }

        public int? MovementGroup { get; set; }

        public int? OverTime { get; set; }

        public DateTime? Missed { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace APIConfig.Models;

public partial class RdActualPlan
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

    public string? CapName { get; set; }

    public string? CapType { get; set; }

    public int? CapQty { get; set; }

    public int? CapHour { get; set; }

    public int? MonthCount { get; set; }

    public string? OptionsSelect { get; set; }
}

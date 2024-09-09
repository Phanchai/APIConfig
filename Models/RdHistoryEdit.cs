using System;
using System.Collections.Generic;

namespace APIConfig.Models;

public partial class RdHistoryEdit
{
    public int CountHis { get; set; }

    public int Article { get; set; }

    public int? CapQty { get; set; }

    public string? CapName { get; set; }

    public string? CapType { get; set; }

    public int? CapHour { get; set; }

    public string? Remark { get; set; }

    public int? CodeProject { get; set; }

    public string? ProjectName { get; set; }

    public string? InitialsBrand { get; set; }

    public string? TypeNames { get; set; }

    public int? Ot { get; set; }

    public string? NameApprove { get; set; }

    public string? DraftmanName { get; set; }

    public int? DraftmanCode { get; set; }

    public string? OptionsSelect { get; set; }

    public DateTime DateSave { get; set; }

    public DateTime DtimeSave { get; set; }

    public int? JobNot { get; set; }

    public int? JobFinish { get; set; }

    public int? MovementGroup { get; set; }
}

using System;
using System.Collections.Generic;

namespace APIConfig.Models;

public partial class RdListReport
{
    public string? NameReports { get; set; }

    public DateOnly? DateCurrent { get; set; }

    public string? SsrsReports { get; set; }
}

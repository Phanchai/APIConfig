using System;
using System.Collections.Generic;

namespace APIConfig.Models;

public partial class RdCapStandard
{
    public int CapId { get; set; }

    public string CapCode { get; set; } = null!;

    public string? CapName { get; set; }

    public string? GroupCap { get; set; }

    public int? CapStandard { get; set; }

    public int? CapCopy { get; set; }
}

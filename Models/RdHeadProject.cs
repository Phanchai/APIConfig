using System;
using System.Collections.Generic;

namespace APIConfig.Models;

public partial class RdHeadProject
{
    public string ProjectName { get; set; } = null!;

    public int CodeProject { get; set; }

    public string? ProjectOwner { get; set; }

    public string? Deskip { get; set; }

    public DateTime? DateGet { get; set; }

    public DateTime? DateFinal { get; set; }

    public string? InitialsBrand { get; set; }
}

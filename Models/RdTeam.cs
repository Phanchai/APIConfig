using System;
using System.Collections.Generic;

namespace APIConfig.Models;

  public partial class RdTeam
{
    public string TeamName { get; set; } = null!;

    public int TeamCode { get; set; }

    public string? BossName { get; set; }

    public int? BossId { get; set; }
}

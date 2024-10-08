﻿using System;
using System.Collections.Generic;

namespace APIConfig.Models;

public partial class UserManager
{
    public int Uid{ get; set; } 
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MidleName {get;set;}
    public string? PasswordHash{get; set; }
    public string? ipAddress { get; set; }
}

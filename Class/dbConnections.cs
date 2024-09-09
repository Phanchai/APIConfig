using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConfig.Models;

namespace APIConfig.Class
{
	public partial class dbConnections
	{
        public readonly ProjectManager2Context _context;

        public dbConnections(ProjectManager2Context context)
        {
            _context = context;
        }
    }
}


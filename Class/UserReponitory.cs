using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Models;

namespace APIConfig.Class
{
    public class UserReponitory
    {
        private readonly ProjectManager2Context _context;
        public UserReponitory(ProjectManager2Context context){
            _context = context;
        }

        public UserManager GetUserManager(string username){
            return _context.UserManagers.SingleOrDefault(u => u.Username == username);
        }
    }
}
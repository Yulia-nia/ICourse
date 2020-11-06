using ICourses.Data.Interfases;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public User GetUserDB(string id)
        {
            return _appDbContext.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}

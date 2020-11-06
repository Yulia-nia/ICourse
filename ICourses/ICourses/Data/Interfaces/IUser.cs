using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfases
{
    public interface IUser
    {
        User GetUserDB(string id);


        /*void AddUserDB(User user);
        IEnumerable<User> GetAllUsers();
        void DeleteUser(User user);
        void DeleteUserById(int id);
        User GetUserDB(int id);
        void UpdateUser(User user);*/
    }
}

using SubwayEntrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayEntrance.Data
{
    public interface IAuthRepository<T>
    {
        User GetById(int id);
        string Register(User user, string password);

        Task<User> LogIn(string UserName, string password);

        Task<bool> ExistUserName(string user);
    }
}

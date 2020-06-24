using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;

namespace Winner.IRepository
{
    public interface IUserService
    {
        Task<User> Login(string name, string password);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Winner.Models;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

using System.IO;

using Winner.IRepository;

namespace Winner.Repository
{
    public class UserService:IUserService
    {
        private readonly AccountContext _context;
        private readonly ILogger _logger;

        public UserService(AccountContext accountContext)
        {
            _context = accountContext;
        }
        public async Task<User> Login(string name,string password)
        {
            var Md5Pwd = Winner.Extends.ExtentionsClass.GetMd5Hash(password.Trim());
            //var user = await _context.User.SingleOrDefaultAsync(s=>s.username.Equals(name.Trim(),StringComparison.CurrentCultureIgnoreCase) && s.password.Equals(Md5Pwd));
            var user = await _context.User.FirstOrDefaultAsync(s => s.UserName.Equals(name) && s.Password.Equals(Md5Pwd));

            if (user!=null)
            {
                return user;
            }
            return null;
        }
    }
}

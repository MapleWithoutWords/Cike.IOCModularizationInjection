using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cike.ModuleInject.TestService.Services
{
    public class UserService : IUserService
    {
        public async Task<string> GetUserNameAsync(string userName)
        {
            return userName;
        }
    }
}

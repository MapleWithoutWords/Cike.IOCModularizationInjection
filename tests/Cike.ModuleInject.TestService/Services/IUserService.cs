using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cike.ModuleInject.TestService.Services
{
    public interface IUserService : ITransientInjection
    {
        public Task<String> GetUserNameAsync(string userName);
    }


}

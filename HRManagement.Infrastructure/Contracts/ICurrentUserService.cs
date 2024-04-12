using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Contracts
{
    public interface ICurrentUserService : IBaseService
    {
        public IdentityUser User { get; set; }
    }
}

using HRManagement.Infrastructure.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public IdentityUser User { get; set; }
    }
}

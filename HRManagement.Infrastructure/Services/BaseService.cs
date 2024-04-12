using HRManagement.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Services
{
    public class BaseService
    {
        protected readonly ICurrentUserService _currentUserService;
        public BaseService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
    }
}

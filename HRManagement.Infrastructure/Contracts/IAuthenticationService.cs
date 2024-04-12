using HRManagement.Domain.DTO.Common.Request;
using HRManagement.Domain.DTO.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Infrastructure.Contracts
{
    public interface IAuthenticationService : IBaseService
    {
        Task<TokenDTO> LogIn(LogInDTO logInInfo);
        Task<TokenDTO> Register(RegisterDTO registerInfo);
    }
}

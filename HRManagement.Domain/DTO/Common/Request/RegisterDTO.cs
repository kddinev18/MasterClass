using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Domain.DTO.Common.Request
{
    public class RegisterDTO : LogInDTO
    {
        public string Email { get; set; } = null!;
    }
}

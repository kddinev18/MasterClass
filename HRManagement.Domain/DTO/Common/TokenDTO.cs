using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Domain.DTO.Common
{
    public class TokenDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
    }
}

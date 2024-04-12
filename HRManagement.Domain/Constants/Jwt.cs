using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Domain.Constants
{
    public static class Jwt
    {
        public static string Key => $"{nameof(Jwt)}:{nameof(Key)}";
        public static string Issuer => $"{nameof(Jwt)}:{nameof(Issuer)}";
        public static string Audience => $"{nameof(Jwt)}:{nameof(Audience)}";
    }
}

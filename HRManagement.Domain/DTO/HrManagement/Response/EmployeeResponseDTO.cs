using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Domain.DTO.HrManagement.Response
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ManagerName { get; set; }
        public string JobTitle { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
    }
}

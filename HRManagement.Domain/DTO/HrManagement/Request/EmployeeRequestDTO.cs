using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Domain.DTO.HrManagement.Request
{
    public class EmployeeRequestDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int? ManagerId { get; set; }
        public bool JobExsists { get; set; }
        public int? JobId { get; set; }
        public string? JobTitle { get; set; }
        public int DepartmentId { get; set; }

    }
}

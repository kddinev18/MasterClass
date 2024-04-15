using HRManagement.Domain.Filters.Base;

namespace HRManagement.Domain.Filters
{
    public class EmployeeFilters : IFilter
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? HireDate { get; set; }
        public int? JobId { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }
    }
}

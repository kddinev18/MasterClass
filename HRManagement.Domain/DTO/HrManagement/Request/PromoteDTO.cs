namespace HRManagement.Domain.DTO.HrManagement.Request
{
    public class PromoteEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public int NewJobId { get; set; }
        public int? NewDepartmentId { get; set; }
    }
}

namespace HRManagement.Domain.DTO.HrManagement.Request
{
    public class PromoteDTO
    {
        public int EmployeeId { get; set; }
        public int NewJobId { get; set; }
        public int? NewDepartment { get; set; }
    }
}

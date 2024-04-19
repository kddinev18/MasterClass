using HRManagement.Domain.DTO.Common;
using HRManagement.Domain.DTO.Common.Base;

namespace HRManagement.Domain.DTO.HrManagement.Response
{
    public class EmployeeResponseDTO : IResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public NomenclatureDTO<int>? Manager { get; set; }
        public NomenclatureDTO<int> Job { get; set; } = null!;
        public NomenclatureDTO<int> Department { get; set; } = null!;
        public List<NomenclatureDTO<int>> PreviousJobs { get; set; } = new List<NomenclatureDTO<int>>();

    }
}

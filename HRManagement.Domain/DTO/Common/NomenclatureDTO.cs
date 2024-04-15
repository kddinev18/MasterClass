namespace HRManagement.Domain.DTO.Common
{
    public class NomenclatureDTO<T>
    {
        public T Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}

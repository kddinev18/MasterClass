namespace HRManagement.Domain.DTO.Common.Base
{
    public class BaseCollectionResponse<T> where T : class, IResponseModel
    {
        public IQueryable<T> Items { get; set; }
        public int Count { get; set; }
    }
}

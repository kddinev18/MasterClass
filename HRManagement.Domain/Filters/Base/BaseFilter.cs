namespace HRManagement.Domain.Filters.Base
{
    public class BaseFilter<T> where T : class, IFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public T? Filters { get; set; }
    }
}

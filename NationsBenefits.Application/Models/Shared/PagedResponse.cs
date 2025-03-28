namespace NationsBenefits.Application.Models.Shared
{
    public class PagedResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T>? Items { get; set; }
        public int TotalPages { get; set; }
    }
}

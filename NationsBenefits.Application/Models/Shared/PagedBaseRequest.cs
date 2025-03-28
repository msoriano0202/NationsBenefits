using NationsBenefits.Application.Constants;

namespace NationsBenefits.Application.Models.Shared
{
    public class PagedBaseRequest
    {
        //public string? Search { get; set; }
        //public string? Sort { get; set; }
        public int Page { get; set; } = ConstantValues.DefaultPage;

        private const int MaxPageSize = ConstantValues.DefaultMaxPageSize;
        private int _pageSize = ConstantValues.DefaultMinPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}

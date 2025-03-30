using NationsBenefits.Application.Constants;

namespace NationsBenefits.Application.Specifications
{
    public abstract class SpecificationParams
    {
        public int Page { get; set; } = PagingValues.DefaultPage;

        private const int MaxPageSize = PagingValues.DefaultMaxPageSize;
        private int _pageSize = PagingValues.DefaultMinPageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}

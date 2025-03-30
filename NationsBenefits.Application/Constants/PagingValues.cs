namespace NationsBenefits.Application.Constants
{
    public class PagingValues
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 10;
        public const int DefaultMinPageSize = 3;
        public const int DefaultMaxPageSize = 50;
    }

    public class RedisValues
    {
        public const string SubCategoriesKey = "SubCategories";
        public const string ProductsKey = "Products";
        public const int CacheTimeInMinutes = 5;
    }
}

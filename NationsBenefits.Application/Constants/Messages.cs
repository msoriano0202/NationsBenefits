namespace NationsBenefits.Application.Constants
{
    public static class ErrorMessages
    {
        public const string EntityNotFound = $"Entity {{0}} with id: {{1}} not found";
        public const string EntityNotExists = $"Entity {{0}} with id: {{1}} not exists";
        public const string EntityNotInserted = $"{{0}} record not inserted";
        public const string ProductsRelatedToSubCategory = $"There are {{0}} Products linked to SubCategoryId: {{1}}";
        public const string BulkInsertError = $"Bulk Insert Error {{0}}";
    }

    public static class SuccessMessages
    {
        public const string EntityInserted = $"{{0}} record was inserted";
        public const string EntityUpdated = $"{{0}} with id: {{1}} was updated succesfully";
        public const string EntityDeleted = $"{{0}} with id: {{1}} was deleted succesfully";
    }
}

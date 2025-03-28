namespace NationsBenefits.Application.Models
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int Category_Id { get; set; }
    }
}

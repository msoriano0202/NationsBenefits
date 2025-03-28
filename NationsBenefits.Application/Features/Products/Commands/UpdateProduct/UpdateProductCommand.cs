using MediatR;

namespace NationsBenefits.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int Subcategory_id { get; set; }
        public string? Ski { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

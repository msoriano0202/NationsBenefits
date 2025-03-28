using MediatR;

namespace NationsBenefits.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public int Subcategory_id { get; set; }
        public string? Ski { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

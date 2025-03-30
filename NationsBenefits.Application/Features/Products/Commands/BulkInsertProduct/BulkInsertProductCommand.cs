using MediatR;
using NationsBenefits.Application.Features.Products.Commands.CreateProduct;

namespace NationsBenefits.Application.Features.Products.Commands.BulkInsertProduct
{
    public class BulkInsertProductCommand : IRequest<bool>
    {
        public List<CreateProductCommand> Data { get; set; } = new List<CreateProductCommand>();
    }
}

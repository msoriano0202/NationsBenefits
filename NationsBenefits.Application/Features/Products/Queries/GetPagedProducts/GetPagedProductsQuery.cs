using MediatR;
using NationsBenefits.Application.Models;
using NationsBenefits.Application.Models.Shared;

namespace NationsBenefits.Application.Features.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQuery: PagedBaseRequest, IRequest<PagedResponse<ProductDto>>
    {
        public string? Name { get; set; }
    }
}

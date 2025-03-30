using MediatR;
using Microsoft.AspNetCore.Mvc;
using NationsBenefits.Application.Features.Products.Commands.BulkInsertProduct;
using NationsBenefits.Application.Features.Products.Commands.CreateProduct;
using NationsBenefits.Application.Features.Products.Commands.DeleteProduct;
using NationsBenefits.Application.Features.Products.Commands.UpdateProduct;
using NationsBenefits.Application.Features.Products.Queries.GetPagedProducts;
using NationsBenefits.Application.Features.Products.Queries.GetProductById;
using NationsBenefits.Application.Models;
using NationsBenefits.Application.Models.Shared;

namespace NationsBenefits.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("paged", Name = "PagedProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ProductDto>))]
        public async Task<ActionResult<PagedResponse<ProductDto>>> GetPagedProducts(
            [FromQuery] GetPagedProductsQuery pagedProductsQuery)
        {
            var pagedProducts = await _mediator.Send(pagedProductsQuery);
            return Ok(pagedProducts);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateProduct([FromBody] CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("BulkInsert", Name = "BulkInsertProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> BulkInsertProduct([FromBody] BulkInsertProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

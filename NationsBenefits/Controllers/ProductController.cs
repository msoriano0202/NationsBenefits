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

        /// <summary>
        /// Get List of all Products paged
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/Product/Paged
        ///     {
        ///         "Name": null,
        ///         "Page": 1,
        ///         "PageSize": 10
        ///     }
        ///  
        /// </remarks>
        /// <param name="pagedProductsQuery"></param>
        /// <returns>
        /// <response code="200">Successful</response>
        /// </returns>
        [HttpGet("paged", Name = "PagedProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ProductDto>))]
        public async Task<ActionResult<PagedResponse<ProductDto>>> GetPagedProducts(
            [FromQuery] GetPagedProductsQuery pagedProductsQuery)
        {
            var pagedProducts = await _mediator.Send(pagedProductsQuery);
            return Ok(pagedProducts);
        }

        /// <summary>
        /// Get a Product by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/Product/123
        ///     
        /// </remarks> 
        /// <param name="id"></param>
        /// <returns>
        /// <response code="200">Successful</response>
        /// <response code="404">Subcategory not found</response>
        /// </returns>
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        /// <summary>
        /// Create a new Product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/Product
        ///     {
        ///         "SubcategoryId": 1,
        ///         "Ski": "Test",
        ///         "Name": "Test",
        ///         "Description": "Test"
        ///     }
        ///     
        /// </remarks> 
        /// <param name="command"></param>
        /// <returns>
        /// <response code="200">Successful</response>
        /// <response code="400">Request Data not consistent</response>
        /// </returns>
        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateProduct([FromBody] CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Update an existing Product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/v1/Product
        ///     {
        ///         "id": 1
        ///         "SubcategoryId": 1,
        ///         "Ski": "Test",
        ///         "Name": "Test",
        ///         "Description": "Test"
        ///     }
        ///     
        /// </remarks> 
        /// <param name="command"></param>
        /// <returns>
        /// <response code="204">Successful</response>
        /// <response code="404">Subcategory not found</response>
        /// <response code="400">Request Data invalid</response>
        /// </returns>
        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete existing Product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1/Product/123
        ///     
        /// </remarks> 
        /// <param name="id"></param>
        /// <returns>
        /// <response code="204">Successful</response>
        /// <response code="404">Subcategory not found</response>
        /// <response code="400">Request Data invalid</response>
        /// </returns>
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

        /// <summary>
        /// Bulk inserting Products 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/Product/BulkInsert
        ///     {
        ///         "data": [
        ///             {
        ///                 "SubcategoryId": 1,
        ///                 "Ski": "Test 1",
        ///                 "Name": "Test 1",
        ///                 "Description": "Test 1"
        ///             },
        ///             {
        ///                 "SubcategoryId": 1,
        ///                 "Ski": "Test 2",
        ///                 "Name": "Test 2",
        ///                 "Description": "Test 2"
        ///             }
        ///         ]
        ///     }
        ///     
        /// </remarks> 
        /// <param name="command"></param>
        /// <returns>
        /// <response code="200">Successful</response>
        /// <response code="400">Request Data invalid</response>
        /// </returns>
        [HttpPost("BulkInsert", Name = "BulkInsertProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> BulkInsertProduct([FromBody] BulkInsertProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

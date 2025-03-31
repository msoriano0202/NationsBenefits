using MediatR;
using Microsoft.AspNetCore.Mvc;
using NationsBenefits.Application.Features.SubCategories.Commands.BulkInsertSubCategory;
using NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory;
using NationsBenefits.Application.Features.SubCategories.Commands.DeleteSubCategory;
using NationsBenefits.Application.Features.SubCategories.Commands.UpdateSubCategory;
using NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategories;
using NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategoryById;
using NationsBenefits.Application.Models;

namespace NationsBenefits.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<SubCategoryController> _logger;

        public SubCategoryController(IMediator mediator, ILogger<SubCategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get List of all Subcategories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/SubCategory
        ///  
        /// </remarks>
        /// <returns>
        /// <response code="200">Successful</response>
        /// </returns>
        [HttpGet(Name = "GetSubcategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<SubCategoryDto>))]
        public async Task<ActionResult<IReadOnlyList<SubCategoryDto>>> GetSubcategories()
        {
            var getSubCategoriesQuery = new GetSubCategoriesQuery();
            var subCategories = await _mediator.Send(getSubCategoriesQuery);
            return Ok(subCategories);
        }

        /// <summary>
        /// Get a Subcategory by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/SubCategory/123
        ///     
        /// </remarks> 
        /// <param name="id"></param>
        /// <returns>
        /// <response code="200">Successful</response>
        /// <response code="404">Subcategory not found</response>
        /// </returns>
        [HttpGet("{id}", Name = "GetSubCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetSubCategoryById(int id)
        {
            var query = new GetSubCategoryByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        /// <summary>
        /// Create a new Subcategory
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/SubCategory
        ///     {
        ///         "Code": "001",
        ///         "Description": "Test",
        ///         "CategoryId": 1
        ///     }
        ///     
        /// </remarks> 
        /// <param name="command"></param>
        /// <returns>
        /// <response code="200">Successful</response>
        /// <response code="400">Request Data not consistent</response>
        /// </returns>
        [HttpPost(Name = "CreateSubCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateSubCategory([FromBody] CreateSubCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Update an existing Subcategory
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/v1/SubCategory
        ///     {
        ///         "id": 1
        ///         "Code": "001",
        ///         "Description": "Test",
        ///         "CategoryId": 1
        ///     }
        ///     
        /// </remarks> 
        /// <param name="command"></param>
        /// <returns>
        /// <response code="204">Successful</response>
        /// <response code="404">Subcategory not found</response>
        /// <response code="400">Request Data invalid</response>
        /// </returns>
        [HttpPut(Name = "UpdateSubCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete existing Subcategory
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1/SubCategory/123
        ///     
        /// </remarks> 
        /// <param name="id"></param>
        /// <returns>
        /// <response code="204">Successful</response>
        /// <response code="404">Subcategory not found</response>
        /// <response code="400">Request Data invalid</response>
        /// </returns>
        [HttpDelete("{id}", Name = "DeleteSubCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteSubCategory(int id)
        {
            var command = new DeleteSubCategoryCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Bulk inserting Subcategories 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/SubCategory/BulkInsert
        ///     {
        ///         "data": [
        ///             {
        ///                 "Code": "abc",
        ///                 "Description": "Test A",
        ///                 "CategoryId": 1
        ///             },
        ///             {
        ///                 "Code": "def",
        ///                 "Description": "Test B",
        ///                 "CategoryId": 1
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
        [HttpPost("BulkInsert", Name = "BulkInsertSubCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> BulkInsertSubCategory([FromBody] BulkInsertSubCategoryCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

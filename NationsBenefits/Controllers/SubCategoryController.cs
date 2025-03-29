using MediatR;
using Microsoft.AspNetCore.Mvc;
using NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory;
using NationsBenefits.Application.Features.SubCategories.Commands.DeleteSubCategory;
using NationsBenefits.Application.Features.SubCategories.Commands.UpdateSubCategory;
using NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategories;
using NationsBenefits.Application.Features.SubCategories.Queries.GetSubCategoryById;
using NationsBenefits.Application.Models;
using System.Net;

namespace NationsBenefits.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private IMediator _mediator;

        public SubCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetSubcategories")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IReadOnlyList<SubCategoryDto>))]
        public async Task<ActionResult<IReadOnlyList<SubCategoryDto>>> GetSubcategories()
        {
            var getSubCategoriesQuery = new GetSubCategoriesQuery();
            var subCategories = await _mediator.Send(getSubCategoriesQuery);
            return Ok(subCategories);
        }

        [HttpGet("{id}", Name = "GetSubCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetSubCategoryById(int id)
        {
            var query = new GetSubCategoryByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpPost(Name = "CreateSubCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateSubCategory([FromBody] CreateSubCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(Name = "UpdateSubCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

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
    }
}

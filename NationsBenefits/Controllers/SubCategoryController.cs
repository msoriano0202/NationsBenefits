using MediatR;
using Microsoft.AspNetCore.Mvc;
using NationsBenefits.Application.Features.SubCategories.Queries;
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
    }
}

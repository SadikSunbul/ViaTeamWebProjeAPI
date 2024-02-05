using Application.Features.FeaturedSectionEntities.Commands.Create;
using Application.Features.FeaturedSectionEntities.Commands.Delete;
using Application.Features.FeaturedSectionEntities.Commands.Update;
using Application.Features.FeaturedSectionEntities.Queries.GetById;
using Application.Features.FeaturedSectionEntities.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeaturedSectionEntitiesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFeaturedSectionEntitieCommand createFeaturedSectionEntitieCommand)
    {
        CreatedFeaturedSectionEntitieResponse response = await Mediator.Send(createFeaturedSectionEntitieCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFeaturedSectionEntitieCommand updateFeaturedSectionEntitieCommand)
    {
        UpdatedFeaturedSectionEntitieResponse response = await Mediator.Send(updateFeaturedSectionEntitieCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFeaturedSectionEntitieResponse response = await Mediator.Send(new DeleteFeaturedSectionEntitieCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFeaturedSectionEntitieResponse response = await Mediator.Send(new GetByIdFeaturedSectionEntitieQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFeaturedSectionEntitieQuery getListFeaturedSectionEntitieQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFeaturedSectionEntitieListItemDto> response = await Mediator.Send(getListFeaturedSectionEntitieQuery);
        return Ok(response);
    }
}
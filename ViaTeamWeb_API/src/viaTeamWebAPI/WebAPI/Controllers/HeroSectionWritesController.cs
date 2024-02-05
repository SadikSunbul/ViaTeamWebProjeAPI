using Application.Features.HeroSectionWrites.Commands.Create;
using Application.Features.HeroSectionWrites.Commands.Delete;
using Application.Features.HeroSectionWrites.Commands.Update;
using Application.Features.HeroSectionWrites.Queries.GetById;
using Application.Features.HeroSectionWrites.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeroSectionWritesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateHeroSectionWriteCommand createHeroSectionWriteCommand)
    {
        CreatedHeroSectionWriteResponse response = await Mediator.Send(createHeroSectionWriteCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateHeroSectionWriteCommand updateHeroSectionWriteCommand)
    {
        UpdatedHeroSectionWriteResponse response = await Mediator.Send(updateHeroSectionWriteCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedHeroSectionWriteResponse response = await Mediator.Send(new DeleteHeroSectionWriteCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdHeroSectionWriteResponse response = await Mediator.Send(new GetByIdHeroSectionWriteQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListHeroSectionWriteQuery getListHeroSectionWriteQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListHeroSectionWriteListItemDto> response = await Mediator.Send(getListHeroSectionWriteQuery);
        return Ok(response);
    }
}
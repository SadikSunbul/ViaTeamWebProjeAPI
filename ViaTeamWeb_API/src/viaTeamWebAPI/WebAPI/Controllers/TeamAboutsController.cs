using Application.Features.TeamAbouts.Commands.Create;
using Application.Features.TeamAbouts.Commands.Delete;
using Application.Features.TeamAbouts.Commands.Update;
using Application.Features.TeamAbouts.Queries.GetById;
using Application.Features.TeamAbouts.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamAboutsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTeamAboutCommand createTeamAboutCommand)
    {
        CreatedTeamAboutResponse response = await Mediator.Send(createTeamAboutCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTeamAboutCommand updateTeamAboutCommand)
    {
        UpdatedTeamAboutResponse response = await Mediator.Send(updateTeamAboutCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTeamAboutResponse response = await Mediator.Send(new DeleteTeamAboutCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTeamAboutResponse response = await Mediator.Send(new GetByIdTeamAboutQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTeamAboutQuery getListTeamAboutQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTeamAboutListItemDto> response = await Mediator.Send(getListTeamAboutQuery);
        return Ok(response);
    }
}
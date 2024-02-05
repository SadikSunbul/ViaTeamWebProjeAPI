using Application.Features.TeamMemberPresentations.Commands.Create;
using Application.Features.TeamMemberPresentations.Commands.Delete;
using Application.Features.TeamMemberPresentations.Commands.Update;
using Application.Features.TeamMemberPresentations.Queries.GetById;
using Application.Features.TeamMemberPresentations.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberPresentationsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTeamMemberPresentationCommand createTeamMemberPresentationCommand)
    {
        CreatedTeamMemberPresentationResponse response = await Mediator.Send(createTeamMemberPresentationCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTeamMemberPresentationCommand updateTeamMemberPresentationCommand)
    {
        UpdatedTeamMemberPresentationResponse response = await Mediator.Send(updateTeamMemberPresentationCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTeamMemberPresentationResponse response = await Mediator.Send(new DeleteTeamMemberPresentationCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTeamMemberPresentationResponse response = await Mediator.Send(new GetByIdTeamMemberPresentationQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTeamMemberPresentationQuery getListTeamMemberPresentationQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTeamMemberPresentationListItemDto> response = await Mediator.Send(getListTeamMemberPresentationQuery);
        return Ok(response);
    }
}
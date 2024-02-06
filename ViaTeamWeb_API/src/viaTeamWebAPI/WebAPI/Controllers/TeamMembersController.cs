using Application.Features.TeamMembers.Commands.Create;
using Application.Features.TeamMembers.Commands.Delete;
using Application.Features.TeamMembers.Commands.Update;
using Application.Features.TeamMembers.Queries.GetById;
using Application.Features.TeamMembers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMembersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTeamMemberCommand createTeamMemberCommand)
    {
        CreatedTeamMemberResponse response = await Mediator.Send(createTeamMemberCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTeamMemberCommand updateTeamMemberCommand)
    {
        UpdatedTeamMemberResponse response = await Mediator.Send(updateTeamMemberCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTeamMemberResponse response = await Mediator.Send(new DeleteTeamMemberCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTeamMemberResponse response = await Mediator.Send(new GetByIdTeamMemberQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTeamMemberQuery getListTeamMemberQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTeamMemberListItemDto> response = await Mediator.Send(getListTeamMemberQuery);
        return Ok(response);
    }
}
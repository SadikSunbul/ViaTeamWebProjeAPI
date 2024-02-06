using Application.Features.SoftwareSkillMembers.Commands.Create;
using Application.Features.SoftwareSkillMembers.Commands.Delete;
using Application.Features.SoftwareSkillMembers.Commands.Update;
using Application.Features.SoftwareSkillMembers.Queries.GetById;
using Application.Features.SoftwareSkillMembers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SoftwareSkillMembersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Guid softwareSkillId)
    {
        int userId = getUserIdFromRequest();
        CreateSoftwareSkillMemberCommand createSoftwareSkillMemberCommand = new();
        createSoftwareSkillMemberCommand.UserId = userId;
        createSoftwareSkillMemberCommand.SoftwareSkillId = softwareSkillId;

        CreatedSoftwareSkillMemberResponse response = await Mediator.Send(createSoftwareSkillMemberCommand);

        return Created(uri: "", response);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        int userId = getUserIdFromRequest();
        DeletedSoftwareSkillMemberResponse response =
            await Mediator.Send(new DeleteSoftwareSkillMemberCommand { Id = id,UserId = userId});

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSoftwareSkillMemberResponse response =
            await Mediator.Send(new GetByIdSoftwareSkillMemberQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest,Guid MemberId)
    {
        GetListSoftwareSkillMemberQuery getListSoftwareSkillMemberQuery = new() { PageRequest = pageRequest,MemberId = MemberId};
        GetListResponse<GetListSoftwareSkillMemberListItemDto> response =
            await Mediator.Send(getListSoftwareSkillMemberQuery);
        return Ok(response);
    }
}
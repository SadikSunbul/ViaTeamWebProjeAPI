using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Delete;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetBId;
using Application.Features.Members.Queries.GetById;
using Application.Features.Members.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Dtos;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : BaseController
{
   
    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UpdateMemberCommandDTO dto)
    {
        UpdateMemberCommand updateMemberCommand = new UpdateMemberCommand(); updateMemberCommand.UserId=getUserIdFromRequest();
        updateMemberCommand.Job = dto.Job;
        updateMemberCommand.Authirize = dto.Authirize;
        updateMemberCommand.Country = dto.Country;
        UpdatedMemberResponse response = await Mediator.Send(updateMemberCommand);

        return Ok(response);
    }

    // [HttpDelete()]
    // public async Task<IActionResult> Delete()
    // {
    //     int UserId = getUserIdFromRequest();
    //     DeletedMemberResponse response = await Mediator.Send(new DeleteMemberCommand { Id = UserId });
    //
    //     return Ok(response);
    // }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMemberResponse response = await Mediator.Send(new GetByUserIdMemberQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMemberQuery getListMemberQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMemberListItemDto> response = await Mediator.Send(getListMemberQuery);
        return Ok(response);
    }
}
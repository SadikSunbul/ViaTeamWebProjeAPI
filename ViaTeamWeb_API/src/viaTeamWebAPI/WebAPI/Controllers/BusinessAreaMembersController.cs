using Application.Features.BusinessAreaMembers.Commands.Create;
using Application.Features.BusinessAreaMembers.Commands.Delete;
using Application.Features.BusinessAreaMembers.Commands.Update;
using Application.Features.BusinessAreaMembers.Queries.GetById;
using Application.Features.BusinessAreaMembers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Nest;
using WebAPI.Controllers.Dtos;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessAreaMembersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Guid ıd)
    {
        CreateBusinessAreaMemberCommand createBusinessAreaMemberCommand = new();
        createBusinessAreaMemberCommand.BusinessAreaId = ıd;
        createBusinessAreaMemberCommand.UserId = getUserIdFromRequest();
        CreatedBusinessAreaMemberResponse response = await Mediator.Send(createBusinessAreaMemberCommand);

        return Created(uri: "", response);
    }

    // [HttpPut]
    // public async Task<IActionResult> Update([FromBody]UpdateBusinessAreaMemberCommandDTO dto )
    // {
    //     UpdateBusinessAreaMemberCommand updateBusinessAreaMemberCommand = new();
    //     updateBusinessAreaMemberCommand.UserId = getUserIdFromRequest();
    //     updateBusinessAreaMemberCommand.BusinessAreaId = dto.BusinessAreaId;
    //     updateBusinessAreaMemberCommand.Id = dto.Id;
    //     UpdatedBusinessAreaMemberResponse response = await Mediator.Send(updateBusinessAreaMemberCommand);
    //
    //     return Ok(response);
    // }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        int UserId = getUserIdFromRequest();
        
        DeletedBusinessAreaMemberResponse response = await Mediator.Send(new DeleteBusinessAreaMemberCommand { Id = id ,UserId = UserId});

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBusinessAreaMemberResponse response = await Mediator.Send(new GetByIdBusinessAreaMemberQuery { Id = id});
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        int UserId = getUserIdFromRequest();
        
        GetListBusinessAreaMemberQuery getListBusinessAreaMemberQuery = new() { PageRequest = pageRequest , UserId = getUserIdFromRequest()};
        GetListResponse<GetListBusinessAreaMemberListItemDto> response = await Mediator.Send(getListBusinessAreaMemberQuery);
        return Ok(response);
    }
}
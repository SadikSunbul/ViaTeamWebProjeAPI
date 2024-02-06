using Application.Features.ExternalLinks.Commands.Create;
using Application.Features.ExternalLinks.Commands.Delete;
using Application.Features.ExternalLinks.Commands.Update;
using Application.Features.ExternalLinks.Queries.GetById;
using Application.Features.ExternalLinks.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Dtos;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExternalLinksController : BaseController
{
    [HttpPost("Member")]
    public async Task<IActionResult> Add([FromBody]CreateExternalLinkCommandDTOMember dto)
    {
        CreateExternalLinkCommand createExternalLinkCommand = new();
        createExternalLinkCommand.MemberId = dto.MemberId;
        createExternalLinkCommand.Name = dto.Name;
        createExternalLinkCommand.Url = dto.Url;
        CreatedExternalLinkResponse response = await Mediator.Send(createExternalLinkCommand);

        return Created(uri: "", response);
    }
    
    [HttpPost("Team")]
    public async Task<IActionResult> AddTeam([FromBody] CreateExternalLinkCommandDTOTeam dto)
    {
        CreateExternalLinkCommand createExternalLinkCommand = new();
        createExternalLinkCommand.Name = dto.Name;
        createExternalLinkCommand.Url = dto.Url;
        createExternalLinkCommand.TeamId = dto.TeamId;
        CreatedExternalLinkResponse response = await Mediator.Send(createExternalLinkCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Member")]
    public async Task<IActionResult> Update([FromBody] UpdateExternalLinkCommandDTOMember dto)
    {
        UpdateExternalLinkCommand updateExternalLinkCommand = new();
        updateExternalLinkCommand.Id = dto.Id;
        updateExternalLinkCommand.MemberId = dto.MemberId;
        updateExternalLinkCommand.Name = dto.Name;
        updateExternalLinkCommand.Url = dto.Url;
        
        UpdatedExternalLinkResponse response = await Mediator.Send(updateExternalLinkCommand);

        return Ok(response);
    }
    
    [HttpPut("Team")]
    public async Task<IActionResult> UpdateTeam([FromBody] UpdateExternalLinkCommandDTOTeam dto)
    {
        UpdateExternalLinkCommand updateExternalLinkCommand = new();
        updateExternalLinkCommand.Id = dto.Id;
        updateExternalLinkCommand.TeamId = dto.TeamId;
        updateExternalLinkCommand.Name = dto.Name;
        updateExternalLinkCommand.Url = dto.Url;
        
        UpdatedExternalLinkResponse response = await Mediator.Send(updateExternalLinkCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        int UserId = getUserIdFromRequest();
        DeletedExternalLinkResponse response = await Mediator.Send(new DeleteExternalLinkCommand { Id = id,UserId = UserId});

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdExternalLinkResponse response = await Mediator.Send(new GetByIdExternalLinkQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListExternalLinkQuery getListExternalLinkQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListExternalLinkListItemDto> response = await Mediator.Send(getListExternalLinkQuery);
        return Ok(response);
    }
}
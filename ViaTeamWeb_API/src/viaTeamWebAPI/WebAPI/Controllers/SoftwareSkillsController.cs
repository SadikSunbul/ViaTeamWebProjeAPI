using Application.Features.SoftwareSkills.Commands.Create;
using Application.Features.SoftwareSkills.Commands.Delete;
using Application.Features.SoftwareSkills.Commands.Update;
using Application.Features.SoftwareSkills.Queries.GetById;
using Application.Features.SoftwareSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SoftwareSkillsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSoftwareSkillCommand createSoftwareSkillCommand)
    {
        CreatedSoftwareSkillResponse response = await Mediator.Send(createSoftwareSkillCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSoftwareSkillCommand updateSoftwareSkillCommand)
    {
        UpdatedSoftwareSkillResponse response = await Mediator.Send(updateSoftwareSkillCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSoftwareSkillResponse response = await Mediator.Send(new DeleteSoftwareSkillCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSoftwareSkillResponse response = await Mediator.Send(new GetByIdSoftwareSkillQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSoftwareSkillQuery getListSoftwareSkillQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSoftwareSkillListItemDto> response = await Mediator.Send(getListSoftwareSkillQuery);
        return Ok(response);
    }
}
using Application.Features.Alooos.Commands.Create;
using Application.Features.Alooos.Commands.Delete;
using Application.Features.Alooos.Commands.Update;
using Application.Features.Alooos.Queries.GetById;
using Application.Features.Alooos.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlooosController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAloooCommand createAloooCommand)
    {
        CreatedAloooResponse response = await Mediator.Send(createAloooCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAloooCommand updateAloooCommand)
    {
        UpdatedAloooResponse response = await Mediator.Send(updateAloooCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAloooResponse response = await Mediator.Send(new DeleteAloooCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAloooResponse response = await Mediator.Send(new GetByIdAloooQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAloooQuery getListAloooQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAloooListItemDto> response = await Mediator.Send(getListAloooQuery);
        return Ok(response);
    }
}
using Application.Features.BusinessAreas.Commands.Create;
using Application.Features.BusinessAreas.Commands.Delete;
using Application.Features.BusinessAreas.Commands.Update;
using Application.Features.BusinessAreas.Queries.GetById;
using Application.Features.BusinessAreas.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessAreasController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBusinessAreaCommand createBusinessAreaCommand)
    {
        CreatedBusinessAreaResponse response = await Mediator.Send(createBusinessAreaCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBusinessAreaCommand updateBusinessAreaCommand)
    {
        UpdatedBusinessAreaResponse response = await Mediator.Send(updateBusinessAreaCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBusinessAreaResponse response = await Mediator.Send(new DeleteBusinessAreaCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBusinessAreaResponse response = await Mediator.Send(new GetByIdBusinessAreaQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBusinessAreaQuery getListBusinessAreaQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBusinessAreaListItemDto> response = await Mediator.Send(getListBusinessAreaQuery);
        return Ok(response);
    }
}
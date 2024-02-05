using Application.Features.ContactPages.Commands.Create;
using Application.Features.ContactPages.Commands.Delete;
using Application.Features.ContactPages.Commands.Update;
using Application.Features.ContactPages.Queries.GetById;
using Application.Features.ContactPages.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactPagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContactPageCommand createContactPageCommand)
    {
        CreatedContactPageResponse response = await Mediator.Send(createContactPageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContactPageCommand updateContactPageCommand)
    {
        UpdatedContactPageResponse response = await Mediator.Send(updateContactPageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedContactPageResponse response = await Mediator.Send(new DeleteContactPageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdContactPageResponse response = await Mediator.Send(new GetByIdContactPageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContactPageQuery getListContactPageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContactPageListItemDto> response = await Mediator.Send(getListContactPageQuery);
        return Ok(response);
    }
}
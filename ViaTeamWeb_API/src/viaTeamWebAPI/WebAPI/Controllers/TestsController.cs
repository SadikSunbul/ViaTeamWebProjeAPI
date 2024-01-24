using Application.Features.Tests.Commands.Create;
using Application.Features.Tests.Commands.Delete;
using Application.Features.Tests.Commands.Update;
using Application.Features.Tests.Queries.GetById;
using Application.Features.Tests.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTestCommand createTestCommand)
    {
        CreatedTestResponse response = await Mediator.Send(createTestCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTestCommand updateTestCommand)
    {
        UpdatedTestResponse response = await Mediator.Send(updateTestCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTestResponse response = await Mediator.Send(new DeleteTestCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTestResponse response = await Mediator.Send(new GetByIdTestQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTestQuery getListTestQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTestListItemDto> response = await Mediator.Send(getListTestQuery);
        return Ok(response);
    }
}
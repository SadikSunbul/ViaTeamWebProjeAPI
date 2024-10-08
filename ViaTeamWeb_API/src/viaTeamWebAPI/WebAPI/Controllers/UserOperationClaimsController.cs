﻿using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] GetByIdUserOperationClaimQuery getByIdUserOperationClaimQuery)
    {
        GetByIdUserOperationClaimResponse result = await Mediator.Send(getByIdUserOperationClaimQuery);
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserOperationClaimQuery getListUserOperationClaimQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListUserOperationClaimListItemDto> result =
            await Mediator.Send(getListUserOperationClaimQuery);
        return Ok(result);
    }

    [HttpPost] //bir kullanıcıya bir işlem yetkisi (operation claim) eklemek için kullanılır
    public async Task<IActionResult> Add(
        [FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
    {
        CreatedUserOperationClaimResponse result = await Mediator.Send(createUserOperationClaimCommand);
        return Created(uri: "", result);
    }

    [HttpPut] //ullanıcının bir işlem yetkisi (operation claim) güncellemesini temsil eder.
    public async Task<IActionResult> Update(
        [FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
    {
        UpdatedUserOperationClaimResponse result = await Mediator.Send(updateUserOperationClaimCommand);
        return Ok(result);
    }

    [HttpDelete] //siler
    public async Task<IActionResult> Delete(
        [FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
    {
        DeletedUserOperationClaimResponse result = await Mediator.Send(deleteUserOperationClaimCommand);
        return Ok(result);
    }
}
using Application.Features.FeaturedArticleCards.Commands.Create;
using Application.Features.FeaturedArticleCards.Commands.Delete;
using Application.Features.FeaturedArticleCards.Commands.Update;
using Application.Features.FeaturedArticleCards.Queries.GetById;
using Application.Features.FeaturedArticleCards.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeaturedArticleCardsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFeaturedArticleCardCommand createFeaturedArticleCardCommand)
    {
        CreatedFeaturedArticleCardResponse response = await Mediator.Send(createFeaturedArticleCardCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFeaturedArticleCardCommand updateFeaturedArticleCardCommand)
    {
        UpdatedFeaturedArticleCardResponse response = await Mediator.Send(updateFeaturedArticleCardCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFeaturedArticleCardResponse response = await Mediator.Send(new DeleteFeaturedArticleCardCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFeaturedArticleCardResponse response = await Mediator.Send(new GetByIdFeaturedArticleCardQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFeaturedArticleCardQuery getListFeaturedArticleCardQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFeaturedArticleCardListItemDto> response = await Mediator.Send(getListFeaturedArticleCardQuery);
        return Ok(response);
    }
}
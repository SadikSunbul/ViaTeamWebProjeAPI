using Core.Application.Dtos;

namespace Application.Features.Tests.Queries.GetList;

public class GetListTestListItemDto : IDto
{
    public Guid Id { get; set; }
    public string NAME { get; set; }
}
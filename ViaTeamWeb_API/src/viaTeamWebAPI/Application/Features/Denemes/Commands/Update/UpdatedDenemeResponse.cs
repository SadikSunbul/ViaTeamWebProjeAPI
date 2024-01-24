using Core.Application.Responses;

namespace Application.Features.Denemes.Commands.Update;

public class UpdatedDenemeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.TeamAbouts.Commands.Delete;

public class DeletedTeamAboutResponse : IResponse
{
    public Guid Id { get; set; }
}
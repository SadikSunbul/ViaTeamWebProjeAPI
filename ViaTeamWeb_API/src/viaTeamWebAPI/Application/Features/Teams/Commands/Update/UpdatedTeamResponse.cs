using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Teams.Commands.Update;

public class UpdatedTeamResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TeamAboutId { get; set; }
    public TeamAbout? TeamAbout { get; set; }
}
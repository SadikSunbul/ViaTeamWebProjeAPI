using Core.Application.Responses;

namespace Application.Features.TeamAbouts.Commands.Create;

public class CreatedTeamAboutResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title1 { get; set; }
    public string Prg1 { get; set; }
    public string Title2 { get; set; }
    public string Prg2 { get; set; }
}
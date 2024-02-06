namespace WebAPI.Controllers.Dtos;

public class CreateExternalLinkCommandDTOTeam
{
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid TeamId { get; set; }
}
namespace WebAPI.Controllers.Dtos;

public class UpdateExternalLinkCommandDTOTeam
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid TeamId { get; set; }
}
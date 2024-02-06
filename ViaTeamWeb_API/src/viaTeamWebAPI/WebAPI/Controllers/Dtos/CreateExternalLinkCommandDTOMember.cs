namespace WebAPI.Controllers.Dtos;

public class CreateExternalLinkCommandDTOMember
{
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid MemberId { get; set; }
}
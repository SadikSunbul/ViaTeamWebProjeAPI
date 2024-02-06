namespace WebAPI.Controllers.Dtos;

public class UpdateExternalLinkCommandDTOMember
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid MemberId { get; set; }
}
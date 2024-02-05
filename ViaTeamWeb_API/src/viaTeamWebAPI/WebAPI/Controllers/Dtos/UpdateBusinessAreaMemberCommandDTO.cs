namespace WebAPI.Controllers.Dtos;

public class UpdateBusinessAreaMemberCommandDTO
{
    public Guid Id { get; set; }
    public Guid BusinessAreaId { get; set; }
}
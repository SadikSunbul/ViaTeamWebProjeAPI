using Core.Application.Responses;

namespace Application.Features.SoftwareSkills.Commands.Delete;

public class DeletedSoftwareSkillResponse : IResponse
{
    public Guid Id { get; set; }
}
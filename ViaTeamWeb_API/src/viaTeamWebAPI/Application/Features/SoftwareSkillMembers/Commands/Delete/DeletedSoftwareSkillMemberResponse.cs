using Core.Application.Responses;

namespace Application.Features.SoftwareSkillMembers.Commands.Delete;

public class DeletedSoftwareSkillMemberResponse : IResponse
{
    public Guid Id { get; set; }
}
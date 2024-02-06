using Application.Features.SoftwareSkillMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.SoftwareSkillMembers.Queries.GetById;

public class GetByIdSoftwareSkillMemberQuery : IRequest<GetByIdSoftwareSkillMemberResponse>
{
    public Guid Id { get; set; }

    public class GetByIdSoftwareSkillMemberQueryHandler : IRequestHandler<GetByIdSoftwareSkillMemberQuery, GetByIdSoftwareSkillMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;
        private readonly SoftwareSkillMemberBusinessRules _softwareSkillMemberBusinessRules;

        public GetByIdSoftwareSkillMemberQueryHandler(IMapper mapper, ISoftwareSkillMemberRepository softwareSkillMemberRepository, SoftwareSkillMemberBusinessRules softwareSkillMemberBusinessRules)
        {
            _mapper = mapper;
            _softwareSkillMemberRepository = softwareSkillMemberRepository;
            _softwareSkillMemberBusinessRules = softwareSkillMemberBusinessRules;
        }

        public async Task<GetByIdSoftwareSkillMemberResponse> Handle(GetByIdSoftwareSkillMemberQuery request, CancellationToken cancellationToken)
        {
            SoftwareSkillMember? softwareSkillMember = await _softwareSkillMemberRepository.GetAsync(predicate: ssm => ssm.Id == request.Id, cancellationToken: cancellationToken);
            await _softwareSkillMemberBusinessRules.SoftwareSkillMemberShouldExistWhenSelected(softwareSkillMember);

            GetByIdSoftwareSkillMemberResponse response = _mapper.Map<GetByIdSoftwareSkillMemberResponse>(softwareSkillMember);
            return response;
        }
    }
}
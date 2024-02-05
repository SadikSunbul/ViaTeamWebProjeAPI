using Application.Features.BusinessAreaMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BusinessAreaMembers.Queries.GetById;

public class GetByIdBusinessAreaMemberQuery : IRequest<GetByIdBusinessAreaMemberResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBusinessAreaMemberQueryHandler : IRequestHandler<GetByIdBusinessAreaMemberQuery, GetByIdBusinessAreaMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;
        private readonly BusinessAreaMemberBusinessRules _businessAreaMemberBusinessRules;

        public GetByIdBusinessAreaMemberQueryHandler(IMapper mapper, IBusinessAreaMemberRepository businessAreaMemberRepository, BusinessAreaMemberBusinessRules businessAreaMemberBusinessRules)
        {
            _mapper = mapper;
            _businessAreaMemberRepository = businessAreaMemberRepository;
            _businessAreaMemberBusinessRules = businessAreaMemberBusinessRules;
        }

        public async Task<GetByIdBusinessAreaMemberResponse> Handle(GetByIdBusinessAreaMemberQuery request, CancellationToken cancellationToken)
        {
            BusinessAreaMember? businessAreaMember = await _businessAreaMemberRepository.GetAsync(predicate: bam => bam.Id == request.Id, cancellationToken: cancellationToken);
            await _businessAreaMemberBusinessRules.BusinessAreaMemberShouldExistWhenSelected(businessAreaMember);

            GetByIdBusinessAreaMemberResponse response = _mapper.Map<GetByIdBusinessAreaMemberResponse>(businessAreaMember);
            return response;
        }
    }
}
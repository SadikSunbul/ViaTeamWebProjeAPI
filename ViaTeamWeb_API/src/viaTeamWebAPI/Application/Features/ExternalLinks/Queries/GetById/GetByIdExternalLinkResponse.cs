using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.ExternalLinks.Queries.GetById;

public class GetByIdExternalLinkResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid? MemberId { get; set; }
    public Guid? TeamId { get; set; }
    public Member? Member { get; set; }
    public Team? Team { get; set; }
}
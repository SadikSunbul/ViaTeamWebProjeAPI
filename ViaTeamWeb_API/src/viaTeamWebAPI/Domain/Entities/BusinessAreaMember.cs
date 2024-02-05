using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BusinessAreaMember:Entity<Guid>
{
    public Guid BusinessAreaId { get; set; }
    public Guid MemberId { get; set; }
    [NotMapped]
    public BusinessArea? BusinessArea { get; set; }
    [NotMapped]
    public Member? Member { get; set; }
}
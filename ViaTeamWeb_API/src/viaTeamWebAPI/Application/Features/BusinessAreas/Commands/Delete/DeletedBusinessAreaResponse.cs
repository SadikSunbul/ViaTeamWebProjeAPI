using Core.Application.Responses;

namespace Application.Features.BusinessAreas.Commands.Delete;

public class DeletedBusinessAreaResponse : IResponse
{
    public Guid Id { get; set; }
}
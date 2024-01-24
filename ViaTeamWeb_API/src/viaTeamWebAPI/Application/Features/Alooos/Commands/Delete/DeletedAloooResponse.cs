using Core.Application.Responses;

namespace Application.Features.Alooos.Commands.Delete;

public class DeletedAloooResponse : IResponse
{
    public Guid Id { get; set; }
}
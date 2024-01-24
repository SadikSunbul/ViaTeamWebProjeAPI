using Application.Features.Tests.Commands.Create;
using Application.Features.Tests.Commands.Delete;
using Application.Features.Tests.Commands.Update;
using Application.Features.Tests.Queries.GetById;
using Application.Features.Tests.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Tests.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Test, CreateTestCommand>().ReverseMap();
        CreateMap<Test, CreatedTestResponse>().ReverseMap();
        CreateMap<Test, UpdateTestCommand>().ReverseMap();
        CreateMap<Test, UpdatedTestResponse>().ReverseMap();
        CreateMap<Test, DeleteTestCommand>().ReverseMap();
        CreateMap<Test, DeletedTestResponse>().ReverseMap();
        CreateMap<Test, GetByIdTestResponse>().ReverseMap();
        CreateMap<Test, GetListTestListItemDto>().ReverseMap();
        CreateMap<IPaginate<Test>, GetListResponse<GetListTestListItemDto>>().ReverseMap();
    }
}
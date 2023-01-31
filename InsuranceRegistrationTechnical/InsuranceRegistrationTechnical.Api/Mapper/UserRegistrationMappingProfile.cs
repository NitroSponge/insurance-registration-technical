using AutoMapper;
using InsuranceRegistrationTechnical.Api.Dtos;
using InsuranceRegistrationTechnical.Service.Models;

namespace InsuranceRegistrationTechnical.Api.Mapper;

public class UserRegistrationMappingProfile : Profile
{
    public UserRegistrationMappingProfile()
    {
        CreateMap<RegisterUserRequestDto, RegisterUserRequestModel>();
    }
}

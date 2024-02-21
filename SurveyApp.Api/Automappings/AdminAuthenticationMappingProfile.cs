using AutoMapper;
using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Api.Automappings
{
    public class AdminAuthenticationMappingProfile : Profile
    {
        public AdminAuthenticationMappingProfile()
        {
            CreateMap<AdminRegisterDTO, Admin>();
            CreateMap<Admin, AdminRegisterDTO>();
        }
    }
}

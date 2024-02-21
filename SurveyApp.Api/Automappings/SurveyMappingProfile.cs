using AutoMapper;
using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Api.Automappings
{
    public class SurveyMappingProfile : Profile
    {
        public SurveyMappingProfile()
        {
            CreateMap<CreateSurveyDTO, Survey>();
            CreateMap<Survey, CreateSurveyDTO>();
            CreateMap<SurveyResponseDTO, Survey>();
            CreateMap<Survey, SurveyResponseDTO>();
        }

    }
}

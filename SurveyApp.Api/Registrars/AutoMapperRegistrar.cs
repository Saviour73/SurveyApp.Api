
using SurveyApp.Api.Automappings;

namespace SurveyApp.Api.Registrars
{
    public class AutoMapperRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AdminAuthenticationMappingProfile));

        }
    }
}

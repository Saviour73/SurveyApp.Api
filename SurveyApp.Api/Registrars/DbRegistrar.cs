
using Microsoft.EntityFrameworkCore;
using SurveyApp.Dal;

namespace SurveyApp.Api.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            var cs = builder.Services.AddDbContext<AppDatabaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


        }
    }
}

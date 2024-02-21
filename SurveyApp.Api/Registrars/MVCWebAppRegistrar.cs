
namespace SurveyApp.Api.Registrars
{
    public class MVCWebAppRegistrar : IWebApplicationRegistrar
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyApp v1");
                c.RoutePrefix = string.Empty;
            });
           
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            }
    }
    }

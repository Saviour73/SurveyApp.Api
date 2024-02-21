using SurveyApp.Api.Registrars;

namespace SurveyApp.Api.Extension
{
    public static class RegistrarExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationBuilderRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }

        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationRegistrar>(scanningType);
            foreach (var registrar in registrars)
            {
                registrar.RegisterPipelineComponents(app);
            }
        }

        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistrar
        {
            return scanningType.Assembly.GetTypes()  // Get all types from the assembly of the specified type.
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)  // Filter types that are assignable to T, not abstract, and not interfaces.
                .Select(Activator.CreateInstance)  // Create an instance of each type.
                .Cast<T>();  // Cast the instances to the specified interface type T.
        }
    }
}

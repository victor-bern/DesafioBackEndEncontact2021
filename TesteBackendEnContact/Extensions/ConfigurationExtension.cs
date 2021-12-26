using Microsoft.Extensions.Configuration;

namespace TesteBackendEnContact.Extensions
{
    public static class ConfigurationExtension
    {
        public static void LoadConfiguration(this IConfiguration configuration)
        {
            KeyConfiguration.Key = configuration.GetValue<string>("Key");
        }
    }
}

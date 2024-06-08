using Azure.Identity;

namespace AzureKeyVaultCertificates.Configuration
{
    public static class ServiceExtensions
    {
        public static IConfigurationBuilder AzureKeyVaultConfigurationBuilder(this IConfigurationBuilder configurationBuilder, string keyVaultName)
        {
            var configuration = configurationBuilder.Build();

            if (configuration["DOTNET_ENVIRONMENT"] != "Local" || configuration["ASPNETCORE_ENVIRONMENT"] != "Local")
            {
                string kvUri = $"https://{keyVaultName}.vault.azure.net";
                configurationBuilder.AddAzureKeyVault(new Uri(kvUri), new DefaultAzureCredential());
            }
            return configurationBuilder;
        }
    }
}

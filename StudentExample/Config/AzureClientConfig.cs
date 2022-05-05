
using Azure.Identity;
using Microsoft.Extensions.Azure;

namespace StudentExample.Config
{
    public class AzureClientConfig
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAzureClients(builder =>
            {
                // Add a KeyVault client
                builder.AddSecretClient(new Uri(""));

                // Add a Storage account client
                builder.AddBlobServiceClient("");

                // Use DefaultAzureCredential by default
                builder.UseCredential(new DefaultAzureCredential());
            });
        }
    }
}

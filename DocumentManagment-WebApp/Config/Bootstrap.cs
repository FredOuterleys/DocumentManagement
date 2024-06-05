using RestSharp;
using DocumentManagment_WebApp.Repositories;
using DocumentManagment_WebApp.Services;

namespace DocumentManagment_WebApp.Config
{
    public class Bootstrap
    {
        public static IHostBuilder CreateHostBuilder(IHostBuilder host, IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(host, nameof(host));
            ArgumentNullException.ThrowIfNull(config, nameof(config));

            return host.ConfigureServices((context, services) =>
            {
                services.AddScoped(provider => new DocumentManagementAPIRepository(CreateRestClient(config["CSDSDocumentManagementAPI:Url"])));
                services.AddScoped<IDocumentManagementAPIService, DocumentManagementAPIService>();
            });
        }

        private static IRestClient CreateRestClient(string? endpoint)
        {
            ArgumentException.ThrowIfNullOrEmpty(endpoint, nameof(endpoint));

            return new RestClient(endpoint);
        }
    }
}

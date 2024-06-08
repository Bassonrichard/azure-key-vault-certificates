using AzureKeyVaultCertificates;
using AzureKeyVaultCertificates.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configBuilder =>
    {
        configBuilder.AzureKeyVaultConfigurationBuilder("bassonrichard-kv");
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient("GatewayAPI", c =>
        {
            c.BaseAddress = new Uri("https://utilita-mhhsadaptor-dev-gateway.procode.technology");
        })
        .AddClientCertificate(hostContext.Configuration["DataIntegrationPlatform:mTLSCertificate"], hostContext.Configuration["DataIntegrationPlatform:mTLSPassword"]);
        
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
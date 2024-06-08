using System.Security.Cryptography.X509Certificates;

namespace AzureKeyVaultCertificates.Configuration
{
    public static partial class mTLSConfiguration
    {
        // This method adds a client certificate to the HttpClientBuilder using passed parameters.
        public static IHttpClientBuilder AddClientCertificate(this IHttpClientBuilder httpClientBuilder, string base64Certificate, string certificatePassword)
        {
            // Check if the base64Certificate is null or empty
            if (!string.IsNullOrEmpty(base64Certificate))
            {
                // Parse the base64Certificate string
                var certificate = ParseCertificate(base64Certificate, certificatePassword);

                // Use HttpClinetHandler to set client certificate option for SSL connection
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ClientCertificates.Add(certificate);

                // Set the handler lifetime and primary http message handler configuration
                httpClientBuilder.SetHandlerLifetime(Timeout.InfiniteTimeSpan);
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return handler;
                });
            }

            return httpClientBuilder;
        }

        // This is a helper method to decode the base64Certificate and store the certificate as an X509Certificate2 object.
        private static X509Certificate2 ParseCertificate(string base64Certificate, string certificatePassword)
        {
            // Convert the base64Certificate string to byte array
            byte[] pfx = Convert.FromBase64String(base64Certificate);

            // Return the certificate as an X509Certificate2 object
            if (string.IsNullOrEmpty(certificatePassword))
            {
                return new X509Certificate2(pfx);
            }
            else
            {
                return new X509Certificate2(pfx, certificatePassword);
            }
       
        }
    }
}

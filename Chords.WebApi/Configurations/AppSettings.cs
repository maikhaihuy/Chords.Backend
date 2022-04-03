using System.Collections.Generic;

namespace Chords.WebApi.Configurations
{
    public class AppSettings
    {
        internal const string SectionName = "AppSettings";

        public SwaggerSettings Swagger { get; set; } = null!;

        public IdentityServerSettings IdentityServer { get; set; } = null!;

        public JwtSettings Jwt { get; set; }

        public SentrySettings Sentry { get; set; } = null!;

        public RabbitMQSettings RabbitMQ { get; set; } = null!;

        public EmailSettings Email { get; set; } = null!;

        public MinioSettings Minio { get; set; } = null!;

        public CloudSettings Cloud { get; set; } = null!;
        
        public ClientUploadSettings ClientUpload { get; set; } = null!;
    }

    public class SwaggerSettings
    {
        public string ClientId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public Dictionary<string, string> Scopes { get; set; } = new();
    }

    public class IdentityServerSettings
    {
        public string BaseUrl { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public bool RequireHttps { get; set; }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        
        public double AccessTokenExpiryDuration { get; set; }
        
        public double RefreshTokenExpiryDuration { get; set; }
    }

    public class SentrySettings
    {
        public string Dsn { get; set; } = null!;
        public bool IncludeRequestPayload { get; set; } = true;
        public bool SendDefaultPii = true;
        public string MinimumBreadcrumbLevel { get; set; }
        public string MinimumEventLevel { get; set; }
        public bool AttachStackTrace { get; set; } = true;
        public bool Debug { get; set; } = true;
        public string DiagnosticsLevel { get; set; }
    }

    public class RabbitMQSettings
    {
        public string Uri { get; set; } = null!;
        public string VirtualHost { get; set; } = null!;
        public bool SSLEnable { get; set; } = false;
        public string Queue { get; set; } = null!;
        public string RoutingKey { get; set; } = null!;
        public string ExchangeName { get; set; } = null!;
    }

    public class EmailSettings
    {
        public string EmailServer { get; set; }
        public string MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }

    public class MinioSettings
    {
        public string AwsEndpoint { get; set; }
        public string AwsKey { get; set; }
        public string AwsSecret { get; set; }
        public string AwsRegion { get; set; }
        public string AwsBucket { get; set; }
    }

    public class CloudSettings
    {
        public string TempDir { get; set; }
        public string RootDir { get; set; }
        public string Domain { get; set; }
    }
    
    public class ClientUploadSettings
    {
        public string Folder { get; set; }
    }

}
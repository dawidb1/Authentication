namespace Authentication.Data
{
    public static class Variables
    {
        public static class Configuration
        {
            public static readonly string DefaultConnection = "DefaultConnection";
            public static readonly string SecurityKey = "mySecurityKeydsfasdfasfasfasf.in";
            public static readonly string ClientAudience = "Biometria";
            public static readonly string ClientIssuer = ClientAudience;
        }

        public static class Swagger
        {
            public static readonly string Title = "Authentication API";
            public static readonly string Description = "Swagger Authentication API";
            public static readonly string Version = "v1";
            public static readonly string Path = "/swagger/v1/swagger.json";
        }
    }
}

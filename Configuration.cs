namespace Blog;
public static class Configuration
{
    //  TOKEN - JWT - Json Web Token
    public static string JwtKey { get; set; }   = "insira_um_token";
    public static string ApiKeyName             = "api_key";
    public static string ApiKey                 = "curso_api";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Post { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}




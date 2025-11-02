namespace ECommerce.Infrastructure.Service;
public class JWTOptions
{
    public static string SectionName { get; set; } = "JWTOptions";
    public string Key { get; set; }
    public string Issure { get; set; }
    public string Audience { get; set; }
    public int DurationInMinutes { get; set; }
}

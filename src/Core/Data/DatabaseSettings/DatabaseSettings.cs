namespace lw.Core.Data;

public class DatabaseSettings : IDbSettings
{
    public string? Provider { get; set; }
    public string? ConnectionString { get; set; }
}

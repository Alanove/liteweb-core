namespace lw.Core.Data;

public interface IDbSettings
{
    string? Provider { get; set; }
    string? ConnectionString { get; set; }
}

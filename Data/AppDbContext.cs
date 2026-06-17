using Npgsql;
namespace SarprasHelpdesk.Data;
public class AppDbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    
    }
}
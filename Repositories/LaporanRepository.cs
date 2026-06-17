using System.Data;
using SarprasHelpdesk.Repositories;
using SarprasHelpdesk.Data;
using Npgsql;
using SarprasHelpdesk.Models.Entities;
using SarprasHelpdesk.Models.Responses;
using SarprasHelpdesk.Models.Requests;
using SarprasHelpdesk.Models.Constant;

public class LaporanRepository : ILaporanRepository
{
    private readonly AppDbContext _dbContext;
    public LaporanRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateAsync(Laporan laporan)
    {
    
        const string sql = @"
        INSERT INTO laporan(
            no_tiket,
            bidang,
            foto,
            kategori,
            deskripsi,
            status,
            dibuat_pada,
            pelapor
        )VALUES
        (
            @no_tiket,
            @bidang,
            @foto,
            @kategori,
            @deskripsi,
            @status::status_laporan,
            @dibuat_pada,
            @pelapor
        );";

        await using var connection = _dbContext.CreateConnection();
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(sql,connection);

        command.Parameters.AddWithValue("@no_tiket", laporan.No_Tiket);
        command.Parameters.AddWithValue("@bidang", laporan.Bidang);
        command.Parameters.AddWithValue("@foto", laporan.Foto ?? "");
        command.Parameters.AddWithValue("@kategori", laporan.Kategori);
        command.Parameters.AddWithValue("@deskripsi", laporan.Deskripsi);
        command.Parameters.AddWithValue("@status", laporan.Status);
        command.Parameters.AddWithValue("@dibuat_pada", laporan.Dibuat_Pada);
        command.Parameters.AddWithValue("@pelapor", laporan.Pelapor);

        // Console.WriteLine($"No Tiket : {laporan.No_Tiket}");
        // Console.WriteLine($"Status   : {laporan.Status}");
        // Console.WriteLine($"Foto     : {laporan.Foto}");

        await command.ExecuteNonQueryAsync();
    }
    // public async Task<int> CountAllAsync()
    // {
    //     const string sql = 
    //     $@"SELECT COUNT(*) FROM laporan";

    //     await using var connection = _dbContext.CreateConnection();

    //     await connection.OpenAsync();

    //     await using var command = new NpgsqlCommand(sql,connection);
    //     return Convert.ToInt32(
    //         await command.ExecuteScalarAsync()
    //     );
    // }
    public async Task<DashboardSummary> GetDashboardSummaryAsync()
    {
        string sql = $@"
            SELECT COUNT(*) AS total_laporan,
            COUNT(*) FILTER ( WHERE status = '{StatusLaporan.Selesai}') AS total_selesai,

            COUNT(*) FILTER(WHERE status = 'Diteruskan Pihak Ketiga') AS total_diteruskan

            FROM laporan;
        ";

        await using var connection = _dbContext.CreateConnection();

        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(sql,connection);

        await using var reader = await command.ExecuteReaderAsync();
        if(await reader.ReadAsync())
        {
            return new DashboardSummary
            {
                TotalLaporanMasuk = reader.GetInt32(0),
                TotalLaporanSelesai = reader.GetInt32(1),
                TotaldiTeruskan = reader.GetInt32(2)
            };
        }
        return new DashboardSummary();
    }
    // public async Task<int>CountByStatusAsync(string status)
    // {   
    //     const string sql = 
    //     @"SELECT COUNT(*) FROM laporan WHERE status = @status";
    //     await using var connection = _dbContext.CreateConnection();
    //     await connection.OpenAsync();
    //     await using var command = new NpgsqlCommand(sql,connection);
    //     command.Parameters.AddWithValue("status",status);
    //     return Convert.ToInt32(
    //         await command.ExecuteScalarAsync()
    //     );
    // }
    public async Task<IEnumerable<LaporanListResponse>> GetListAsync()
    {
        var result = new List<LaporanListResponse>();
        const string sql = @"
        SELECT 
        l.id,
        l.no_tiket,
        l.pelapor,
        l.kategori,
        l.status,
        l.dibuat_pada
        FROM laporan l ORDER BY l.dibuat_pada DESC";
        await using var connection = _dbContext.CreateConnection();
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            result.Add(new LaporanListResponse
            {
               Id = reader.GetInt32(0), 
               No_Tiket = reader.GetString(1), 
               Pelapor = reader.GetString(2), 
               Kategori = reader.GetString(3), 
               Status = reader.GetString(4), 
               Dibuat_Pada = reader.GetDateTime(5), 
            });
        }
        return result;
    }
    public async Task UpdateStatusAsync(
        UpdateStatusLaporan request)
    {
        const string sql = $@"UPDATE laporan SET status = @status::status_laporan WHERE id = @id";
        await using var connection = _dbContext.CreateConnection();
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", request.Id);
        command.Parameters.AddWithValue("status", request.Status);
        await command.ExecuteNonQueryAsync();
    }
}
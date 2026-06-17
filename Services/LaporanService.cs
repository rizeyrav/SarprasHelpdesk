using SarprasHelpdesk.Models.Entities;
using SarprasHelpdesk.Models.Requests;
using SarprasHelpdesk.Repositories;
using Microsoft.AspNetCore.SignalR;
using SarprasHelpdesk.Hubs;
using SarprasHelpdesk.Models.Responses;

namespace SarprasHelpdesk.Services;
public class LaporanService : ILaporanService
{
    private readonly ILaporanRepository _laporanRepository;
    private readonly IHubContext<NotificationHub> _hubContext;
    public LaporanService(ILaporanRepository laporanRepository,
    IHubContext<NotificationHub> hubContext)
    {
        _laporanRepository = laporanRepository;
        _hubContext = hubContext;
    }
    public async Task CreateAsync(CreateLaporanRequest request)
    {
        string? fileName = null;
        if(request.Foto != null){
        fileName =  $"{Guid.NewGuid()}{Path.GetExtension(request.Foto.FileName)}";
        var uploadFolder =  Path.Combine(
        Directory.GetCurrentDirectory(),
        "wwwroot",
        "uploads");
        Directory.CreateDirectory(uploadFolder);
        var filePath = Path.Combine(uploadFolder, fileName);
        using var stream = new FileStream(filePath,FileMode.Create);

        await request.Foto.CopyToAsync(stream); 
        }
    
        var laporan = new Laporan
        {
            No_Tiket = GenerateTicketNumber(),
            Pelapor = request.Pelapor,
            Bidang =  request.Bidang,
            Kategori = request.Kategori,
            Deskripsi = request.Deskripsi,
            Foto = fileName,
            Status = "Diproses",
            Dibuat_Pada = DateTime.Now
        };
        await _laporanRepository.CreateAsync(laporan);
        await _hubContext.Clients.All.SendAsync(
            "ReceiveNotification",
            laporan.No_Tiket,
            laporan.Pelapor,
            laporan.Kategori
        );
    }
    private string GenerateTicketNumber()
    {
        return $"SPR-{DateTime.Now:yyyyMMddHHmmss}";
    }
    public async Task<DashboardSummary> GetDashboardSummaryAsync()
    {
        return await _laporanRepository.GetDashboardSummaryAsync();
    }
    public async Task<IEnumerable<LaporanListResponse>>GetListAsync()
    {
        return await _laporanRepository.GetListAsync();
    }
    public async Task UpdateStatusAsync(UpdateStatusLaporan request)
    {
        await _laporanRepository.UpdateStatusAsync(request);
    }
}

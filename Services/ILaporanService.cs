using System.Data;
using SarprasHelpdesk.Models.Requests;
using SarprasHelpdesk.Models.Responses;
namespace SarprasHelpdesk.Services;

public interface ILaporanService
{
    Task CreateAsync(CreateLaporanRequest request);
    Task<DashboardSummary> GetDashboardSummaryAsync();
    Task<IEnumerable<LaporanListResponse>>GetListAsync();
    Task UpdateStatusAsync(UpdateStatusLaporan request);
}
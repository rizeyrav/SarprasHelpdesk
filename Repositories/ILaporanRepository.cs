using SarprasHelpdesk.Models.Entities;
using SarprasHelpdesk.Models.Requests;
using SarprasHelpdesk.Models.Responses;
namespace SarprasHelpdesk.Repositories;
public interface ILaporanRepository
{
    Task CreateAsync(Laporan laporan);
    // Task<int> CountAllAsync();
    // Task<int> CountByStatusAsync(string status);
    Task <DashboardSummary> GetDashboardSummaryAsync();
    Task<IEnumerable<LaporanListResponse>>GetListAsync();
    Task UpdateStatusAsync(UpdateStatusLaporan request);
}
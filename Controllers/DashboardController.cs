using Microsoft.AspNetCore.Mvc;
using SarprasHelpdesk.Services;

namespace SarprasHelpdesk.Controllers;

public class DashboardController : Controller
{
    private readonly ILaporanService _laporanServices;

    public DashboardController(ILaporanService laporanService)
    {
        _laporanServices = laporanService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _laporanServices.GetDashboardSummaryAsync();
        return View(model);
    }
}
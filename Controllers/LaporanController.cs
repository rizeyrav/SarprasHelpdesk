using Microsoft.AspNetCore.Mvc;
using SarprasHelpdesk.Models.Requests;
using SarprasHelpdesk.Services;
using SarprasHelpdesk.Repositories;

namespace SarprasHelpdesk.Controllers;

public class LaporanController : Controller
{
    private readonly ILaporanService _laporanService;
    public LaporanController(ILaporanService laporanService)
    {
        _laporanService = laporanService;
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var laporan = await _laporanService.GetListAsync();
        return View(laporan);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateLaporanRequest request)
    {
        if(!ModelState.IsValid)
            return View(request);
        
        await _laporanService.CreateAsync(request);
        TempData["Success"] = " Laporan Berhasil Dikirim";
        return RedirectToAction(nameof(Create));
    }
    [HttpPost]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusLaporan request)
    {
        await _laporanService.UpdateStatusAsync(request);
        return Ok();
    }
}
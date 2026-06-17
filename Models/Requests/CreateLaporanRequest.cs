using System.ComponentModel.DataAnnotations;
namespace SarprasHelpdesk.Models.Requests;
public class CreateLaporanRequest
{
    public int Id { get; set; }

    public string No_Tiket { get; set; } = string.Empty;

    [Required]
    public string Bidang { get; set; } = string.Empty;
    [Required]
    public IFormFile? Foto { get; set; }
    [Required]
    public string Kategori { get; set; } = string.Empty;
    [Required]
    public string Deskripsi { get; set; } = string.Empty;

    public string? Status { get; set; } = string.Empty;

    public DateTime Dibuat_Pada { get; set; }
    [Required]
    public string Pelapor { get; set; } = string.Empty;
}
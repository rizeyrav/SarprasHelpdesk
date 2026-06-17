namespace SarprasHelpdesk.Models.Responses;

public class LaporanListResponse{
   public int Id { get; set; }

    public string No_Tiket { get; set; } = string.Empty;
    public string Pelapor { get; set; } = string.Empty;
    public string Foto { get; set; }= string.Empty;

    public string Kategori { get; set; } = string.Empty;

    public string? Status { get; set; } = string.Empty;

    public DateTime Dibuat_Pada { get; set; }
}
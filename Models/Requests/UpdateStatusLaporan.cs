namespace SarprasHelpdesk.Models.Requests;
public class UpdateStatusLaporan
{
    public int Id{ get; set; }
    public string Status { get; set; } = string.Empty;
}
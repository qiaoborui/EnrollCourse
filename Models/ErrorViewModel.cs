namespace WebApplication2.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    
    public string? reason { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
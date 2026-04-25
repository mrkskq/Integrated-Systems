namespace EventsManagement.Domain.Models;

public class EventsImages
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public long Size { get; set; }
}
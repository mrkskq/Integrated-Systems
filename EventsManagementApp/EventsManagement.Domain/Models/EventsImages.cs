using EventsManagement.Domain.Common;

namespace EventsManagement.Domain.Models;

public class EventsImages : BaseEntity // od aud 5 zaboraeno
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public long Size { get; set; }
}
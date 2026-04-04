using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Extensions;

public static class ConsultationExtensions
{
    // nemat vrska koe e koe (ToBasicResponse ili ToResponse), samo taka se iminjata i da odgovaret parametrite vnatre
    
    // za POST, PUT, DELETE
    public static ConsultationResponse ToBasicResponse(this Consultation consultation)
    {
        return new ConsultationResponse(
            consultation.Id,
            consultation.RoomId,
            consultation.StartTime,
            consultation.EndTime
        );
    }
    
    // za GET
    public static ConsultationDetailsResponse ToResponse(this Consultation consultation)
    {
        return new ConsultationDetailsResponse(
            consultation.Id,
            DateOnly.FromDateTime(consultation.StartTime),
            consultation.RoomId,
            consultation.Room?.Name ?? string.Empty, // ovde morat da se stajt proverka za null zaradi eden test
            consultation.RegisteredStudents,
            consultation.Attendances.Select(x => x.ToBasicResponse()).ToList()
        );
    }
}
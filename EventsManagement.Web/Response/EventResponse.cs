namespace EventsManagement.Web.Response;

public record EventResponse(
    string Name,
    string Description,
    string BannerUrl,
    DateTime StartDate,
    DateTime EndDate,
    string? VenueName,
    string? VenueCity,
    string? VenueCountry
    );
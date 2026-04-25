namespace EventsManagement.Domain.ExternalModels;

public class LegacyVenue
{
    public int VenueId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public int TotalCapacity { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastModified { get; set; }
}


// imame nekoj vakov model od nekoj legacy sistem i za nego prajme model sho ke odgovarat tuka
// deka nasiot model na Venue ne odgovarat so ovaj LegacyVenue, pa morat nov da prajme
// ne nasledvit od Base Entity deka ne e vo takov format so Guid tuku so int e id-to


//CREATE TABLE dbo.Venues (
//     VenueId       INT IDENTITY(1,1) PRIMARY KEY,
//     Name          NVARCHAR(200) NOT NULL,
//     Address       NVARCHAR(500),
//     City          NVARCHAR(100),
//     Country       NVARCHAR(100),
//     ZipCode       NVARCHAR(20),
//     TotalCapacity INT NOT NULL DEFAULT 0,
//     IsActive      BIT NOT NULL DEFAULT 1,
//     LastModified  DATETIME2 NOT NULL DEFAULT GETUTCDATE()
// );


// aud 6, slajd 27 i 28

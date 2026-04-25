namespace EventsManagement.Domain.ExternalModels;

public class LegacySection
{
    public int SectionId { get; set; }
    public int VenueId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public DateTime LastModified { get; set; }
}


// imame nekoj vakov model od nekoj legacy sistem i za nego prajme model sho ke odgovarat tuka
// deka nasiot model na Section ne odgovarat so ovaj LegacySection, pa morat nov da prajme
// ne nasledvit od Base Entity deka ne e vo takov format so Guid tuku so int e id-to


// CREATE TABLE dbo.Sections (
//     SectionId    INT IDENTITY(1,1) PRIMARY KEY,
//     VenueId      INT NOT NULL REFERENCES dbo.Venues(VenueId),
//     Name         NVARCHAR(100) NOT NULL,
//     Capacity     INT NOT NULL DEFAULT 0,
//     LastModified DATETIME2 NOT NULL DEFAULT GETUTCDATE()
// );


// aud 6, slajd 27 i 28
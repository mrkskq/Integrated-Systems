namespace EventsManagement.Domain.ExternalModels;

public class LegacySeat
{
    public int SeatId { get; set; }
    public int SectionId { get; set; }
    public string Row { get; set; }
    public int Number { get; set; }
    public string Label { get; set; }
    public bool IsAccessible { get; set; }
    public DateTime LastModified { get; set; }
}


// imame nekoj vakov model od nekoj legacy sistem i za nego prajme model sho ke odgovarat tuka
// deka nasiot model na Seat ne odgovarat so ovaj LegacySeat, pa morat nov da prajme
// ne nasledvit od Base Entity deka ne e vo takov format so Guid tuku so int e id-to


// CREATE TABLE dbo.Seats (
//     SeatId       INT IDENTITY(1,1) PRIMARY KEY,
//     SectionId    INT NOT NULL REFERENCES dbo.Sections(SectionId),
//     Row          NVARCHAR(10) NOT NULL,
//     Number       INT NOT NULL,
//     Label        NVARCHAR(50),
//     IsAccessible BIT NOT NULL DEFAULT 0,
//     LastModified DATETIME2 NOT NULL DEFAULT GETUTCDATE()
// );
// 


// aud 6, slajd 27 i 28


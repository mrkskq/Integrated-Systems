namespace Web.Response;

/*
За секој од термините за консултации потребно е да се вратат:

Id
ConsultationsDate 
RoomId
RoomName
NumberOfRegistredStudents
Листа на пријавени присуства и за секој од нив (Id, FirstName, LastName)
 */

public record ConsultationDetailsResponse(
    Guid Id,
    DateOnly Date, //-> morat da se vikat "Date" od nekoja pricina za testot da pominit
    Guid RoomId,
    string RoomName,
    int RegisteredStudents, // ne NumberOfRegistredStudents deka vaka e vo modelot
    List<AttendanceShortResponse> Attendances
    );
namespace Web.Response;

/*
 POSLEDNATA LINIJA:
  
За секој од термините за консултации потребно е да се вратат:

Id
ConsultationsDate
RoomId
RoomName
NumberOfRegistredStudents
Листа на пријавени присуства и за секој од нив (Id, FirstName, LastName) <<<<<<<<<<<<
 */

public record AttendanceShortResponse(
    Guid Id,
    string FirstName,
    string LastName
    );
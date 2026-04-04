namespace Web.Response;

/*
Враќа:

Id
UserId
FirstName
LastName
Status - како текстуална вредност
Comment?
 */
public record AttendanceResponse(
    Guid Id,
    string UserId,
    string FirstName,
    string LastName,
    string Status,
    string? Comment
    );
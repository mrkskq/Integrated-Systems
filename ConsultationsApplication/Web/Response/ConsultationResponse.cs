namespace Web.Response;

/*
За новокреираниот термин за консултации потребно е да се вратат:

Id
RoomId
Start
End
 */

public record ConsultationResponse(
 Guid Id,
 Guid RoomId,
 DateTime Start,
 DateTime End
 );
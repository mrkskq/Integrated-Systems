namespace Web.Request;

/*
Параметри кои се испраќаат во телото на барањето:

ConsultationId
UserId
RoomId
Comment?
 */
public record AttendanceRequest(
    Guid ConsultationId,
    string UserId,
    Guid RoomId,
    string? Comment
    );
    
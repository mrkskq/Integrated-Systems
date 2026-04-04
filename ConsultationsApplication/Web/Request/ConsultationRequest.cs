namespace Web.Request;

/*
Параметри кои се испраќаат во телото на барањето:

RoomId
StartTime
EndTime
 */

public record ConsultationRequest(
 Guid RoomId,
 DateTime StartTime,
 DateTime EndTime
 );
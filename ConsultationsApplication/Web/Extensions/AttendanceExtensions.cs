using Domain.Dto;
using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class AttendanceExtensions
{
    // ovde samo ToResponse go koristime
    public static AttendanceResponse ToResponse(this Attendance attendance)
    {
        return new AttendanceResponse(
            attendance.Id,
            attendance.UserId,
            attendance.User.FirstName,
            attendance.User.LastName,
            attendance.Status.ToString(),
            attendance.Comment);
    }
    
    // ova e pomoshno za kaj ConsultationExtensions ko ke zemame lista od attendances najdolu
    public static AttendanceShortResponse ToBasicResponse(this Attendance attendance)
    {
        return new AttendanceShortResponse(
            Id: attendance.Id,
            FirstName: attendance.User.FirstName,
            LastName: attendance.User.LastName
        );
    }
    
}
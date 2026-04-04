using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    // vo controller -> dependency injection na MAPPER, i konstruktor

    private readonly AttendanceMapper _attendanceMapper;

    public AttendanceController(AttendanceMapper attendanceMapper)
    {
        _attendanceMapper = attendanceMapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AttendanceRequest request)
    {
        var result = await _attendanceMapper.RegisterAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _attendanceMapper.DeleteByIdAsync(id);
        return Ok();
    }

    [HttpGet("consultation/{consultationId}")]
    public async Task<IActionResult> GetByConsultation([FromRoute] Guid consultationId)
    {
        var result = await _attendanceMapper.GetAllByConsultationIdAsync(consultationId);
        return Ok(result);
    }

    [HttpPatch("{id}/mark-as-absent")]
    public async Task<IActionResult> MarkAsAbsentAsync([FromRoute] Guid id)
    {
        await _attendanceMapper.MarkAsAbsentAsync(id);
        return Ok();
    }

    [HttpPost("{id}/cancelation-reason")]
    public async Task<IActionResult> UploadReasonByIdInFileSystemAsync([FromRoute] Guid id, [FromForm] IFormFile file)
    {
        var result = await _attendanceMapper.UploadReasonByIdInFileSystemAsync(id, file);
        return Ok(result);
    }
    
}
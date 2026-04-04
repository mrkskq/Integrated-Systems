using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultationController : ControllerBase
{
    // vo controller -> dependency injection na MAPPER, i konstruktor

    private readonly ConsultationMapper _consultationMapper;

    public ConsultationController(ConsultationMapper consultationMapper)
    {
        _consultationMapper = consultationMapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] ConsultationRequest request)
    {
        var result = await _consultationMapper.InsertAsync(request);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<List<ConsultationDetailsResponse>> GetAll([FromQuery] string? roomName, [FromQuery] DateOnly? date)
    {
        return await _consultationMapper.GetAllAsync(roomName, date);
        //return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ConsultationRequest request)
    {
        var result = await _consultationMapper.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _consultationMapper.DeleteAsync(id);
        return Ok(result);
    }
    
    [HttpGet("paged")]
    public async Task<PaginatedResponse<ConsultationDetailsResponse>> GetPaged([FromQuery] PaginatedRequest request)
    {
        return await _consultationMapper.GetPagedAsync(request);
    }
}
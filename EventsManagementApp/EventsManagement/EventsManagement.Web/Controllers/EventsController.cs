using EventsManagement.Web.Mapper;
using EventsManagement.Web.Request;
using EventsManagement.Web.Response;
using Microsoft.AspNetCore.Mvc;

namespace EventsManagement.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventMapper _eventMapper;

    public EventsController(EventMapper eventMapper)
    {
        _eventMapper = eventMapper;
    }
    
    [HttpGet]
    public async Task<List<EventResponse>> GetAll()
    {
        return await _eventMapper.GetAll();
    }

    // /api/Events/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _eventMapper.GetById(id);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] EventRequest request)
    {
        var result = await _eventMapper.InsertAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EventRequest request)
    {
        var result = _eventMapper.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _eventMapper.DeleteAsync(id);
        return Ok(result);
    }
    
    [HttpGet("paged")]
    public async Task<PaginatedResponse<EventResponse>> Paged([FromQuery] PaginatedRequest request)
    {
        return await _eventMapper.PaginatedGetAllAsync(request);
    }

    
}
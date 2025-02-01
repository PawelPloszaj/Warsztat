using WebApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Server.Api;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HistoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{vehicleId}")]
    public async Task<ActionResult<IEnumerable<Repair>>> GetRepairHistory(int vehicleId)
    {
        var history = await _context.Repairs
            .Where(r => _context.RepairOrders
                .Any(ro => ro.Id == r.RepairOrderId && ro.VehicleId == vehicleId))
            .ToListAsync();

        if (!history.Any())
        {
            return NotFound($"No repair history found for vehicle with ID {vehicleId}");
        }

        return Ok(history);
    }
}

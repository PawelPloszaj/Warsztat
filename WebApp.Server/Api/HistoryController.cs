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

    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<RepairOrder>>> GetRepairOrdersForVehicle(int vehicleId)
    {
        var repairOrders = await _context.RepairOrders
            .Where(ro => ro.VehicleId == vehicleId)
            .ToListAsync();

        return Ok(repairOrders);
    }

    [HttpGet("repairorder/{repairOrderId}")]
    public async Task<ActionResult<IEnumerable<Repair>>> GetRepairsForRepairOrder(int repairOrderId)
    {
        var repairs = await _context.Repairs
            .Where(r => r.RepairOrderId == repairOrderId)
            .ToListAsync();

        return Ok(repairs);
    }

    [HttpGet("repair/{repairId}/parts")]
    public async Task<ActionResult<IEnumerable<Part>>> GetPartsForRepair(int repairId)
    {
        var parts = await _context.Parts
            .Where(p => p.RepairId == repairId)
            .ToListAsync();

        return Ok(parts);
    }
}

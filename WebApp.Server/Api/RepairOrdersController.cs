using WebApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Server.Api;

[Route("api/[controller]")]
[ApiController]
public class RepairOrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RepairOrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RepairOrder>>> GetRepairOrders()
    {
        return await _context.RepairOrders.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<RepairOrder>> AddRepairOrder(RepairOrder repairOrder)
    {
        try
        {
            if (repairOrder == null)
            {
                return BadRequest("Repair order is null.");
            }

            if (repairOrder.VehicleId == 0)
            {
                return BadRequest("VehicleId is required.");
            }

            _context.RepairOrders.Add(repairOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRepairOrders), new { id = repairOrder.Id }, repairOrder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding repair order: {ex.Message}");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepairOrder(int id, RepairOrder repairOrder)
    {
        if (id != repairOrder.Id) return BadRequest();

        _context.Entry(repairOrder).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepairOrder(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);
        if (repairOrder == null) return NotFound();

        _context.RepairOrders.Remove(repairOrder);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

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

    [HttpGet("{id}")]
    public async Task<ActionResult<RepairOrder>> GetRepairOrder(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);
        return repairOrder == null ? NotFound() : Ok(repairOrder);
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

            repairOrder.StartDate = DateTime.SpecifyKind(repairOrder.StartDate, DateTimeKind.Utc);

            _context.RepairOrders.Add(repairOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRepairOrder), new { id = repairOrder.Id }, repairOrder);
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
        if (id != repairOrder.Id)
        {
            return BadRequest("Mismatched Repair Order ID.");
        }

        repairOrder.StartDate = DateTime.SpecifyKind(repairOrder.StartDate, DateTimeKind.Utc);
        repairOrder.EndDate = DateTime.SpecifyKind((DateTime)repairOrder.EndDate, DateTimeKind.Utc);

        _context.Entry(repairOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RepairOrderExists(id))
            {
                return NotFound($"Repair order with ID {id} not found.");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepairOrder(int id)
    {
        var repairOrder = await _context.RepairOrders.FindAsync(id);
        if (repairOrder == null)
        {
            return NotFound();
        }

        _context.RepairOrders.Remove(repairOrder);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool RepairOrderExists(int id)
    {
        return _context.RepairOrders.Any(ro => ro.Id == id);
    }
}

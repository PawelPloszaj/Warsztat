using WebApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Server.Api;

[Route("api/[controller]")]
[ApiController]
public class RepairsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RepairsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Repair>>> GetRepairs()
    {
        return await _context.Repairs
            .Include(r => r.Mechanics)
            .Include(r => r.Parts)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Repair>> GetRepair(int id)
    {
        var repair = await _context.Repairs
            .Include(r => r.Mechanics)
            .Include(r => r.Parts)
            .FirstOrDefaultAsync(r => r.Id == id);

        return repair == null ? NotFound() : Ok(repair);
    }

    [HttpPost]
    public async Task<ActionResult<Repair>> AddRepair(Repair repair)
    {
        if (repair == null)
        {
            return BadRequest("Repair data is required.");
        }

        if (repair.RepairOrderId == 0)
        {
            return BadRequest("RepairOrderId is required.");
        }

        var repairOrder = await _context.RepairOrders.FindAsync(repair.RepairOrderId);
        if (repairOrder == null)
        {
            return BadRequest($"RepairOrder with ID {repair.RepairOrderId} does not exist.");
        }

        _context.Repairs.Add(repair);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRepair), new { id = repair.Id }, repair);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepair(int id, Repair repair)
    {
        if (id != repair.Id)
        {
            return BadRequest("Repair ID mismatch.");
        }

        _context.Entry(repair).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RepairExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepair(int id)
    {
        var repair = await _context.Repairs.FindAsync(id);
        if (repair == null)
        {
            return NotFound();
        }

        _context.Repairs.Remove(repair);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{repairId}/assign-mechanic")]
    public async Task<IActionResult> AssignMechanicToRepair(int repairId, [FromBody] AssignMechanicDto request)
    {
        var repair = await _context.Repairs
            .Include(r => r.Mechanics)
            .FirstOrDefaultAsync(r => r.Id == repairId);

        if (repair == null)
        {
            return NotFound($"Repair with ID {repairId} not found.");
        }

        var mechanic = await _context.Mechanics.FindAsync(request.MechanicId);
        if (mechanic == null)
        {
            return NotFound($"Mechanic with ID {request.MechanicId} not found.");
        }

        if (repair.Mechanics.Any(m => m.Id == request.MechanicId))
        {
            return BadRequest("This mechanic is already assigned to the repair.");
        }

        repair.Mechanics.Add(mechanic);
        await _context.SaveChangesAsync();
        return Ok(repair);
    }

    private bool RepairExists(int id)
    {
        return _context.Repairs.Any(e => e.Id == id);
    }

    [HttpGet("{repairId}/mechanics")]
    public async Task<ActionResult<IEnumerable<Mechanic>>> GetAssignedMechanics(int repairId)
    {
        var repair = await _context.Repairs
            .Include(r => r.Mechanics)
            .FirstOrDefaultAsync(r => r.Id == repairId);

        if (repair == null)
        {
            return NotFound($"Repair with ID {repairId} not found.");
        }

        return Ok(repair.Mechanics.ToList());
    }



    [HttpDelete("{repairId}/remove-mechanic/{mechanicId}")]
    public async Task<IActionResult> RemoveMechanicFromRepair(int repairId, int mechanicId)
    {
        var repair = await _context.Repairs
            .Include(r => r.Mechanics)
            .FirstOrDefaultAsync(r => r.Id == repairId);

        if (repair == null)
            return NotFound("Repair not found.");

        var mechanic = repair.Mechanics.FirstOrDefault(m => m.Id == mechanicId);
        if (mechanic == null)
            return NotFound("Mechanic not assigned to this repair.");

        repair.Mechanics.Remove(mechanic);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public class AssignMechanicDto
{
    public int MechanicId { get; set; }
}

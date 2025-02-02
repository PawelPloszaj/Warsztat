using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Data;
namespace WebApp.Server.Api;

[Route("api/[controller]")]
[ApiController]
public class MechanicsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MechanicsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mechanic>>> GetMechanics()
    {
        return await _context.Mechanics.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Mechanic>> GetMechanic(int id)
    {
        var mechanic = await _context.Mechanics.FindAsync(id);
        return mechanic == null ? NotFound() : Ok(mechanic);
    }

    [HttpPost]
    public async Task<ActionResult<Mechanic>> AddMechanic(Mechanic mechanic)
    {
        _context.Mechanics.Add(mechanic);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMechanic), new { id = mechanic.Id }, mechanic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMechanic(int id, Mechanic mechanic)
    {
        if (id != mechanic.Id) return BadRequest();

        _context.Entry(mechanic).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMechanic(int id)
    {
        var mechanic = await _context.Mechanics.FindAsync(id);
        if (mechanic == null) return NotFound();

        _context.Mechanics.Remove(mechanic);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("repairs")]
    public async Task<ActionResult<Dictionary<int, List<Repair>>>> GetRepairsByMechanicAsync()
    {
        var repairsByMechanic = await _context.Mechanics
            .Include(m => m.Repairs)
            .ToListAsync();

        var result = repairsByMechanic.ToDictionary(
            mechanic => mechanic.Id,
            mechanic => mechanic.Repairs.ToList()
        );

        return Ok(result);
    }

}

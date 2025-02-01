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
        return await _context.Repairs.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Repair>> AddRepair(Repair repair)
    {
        _context.Repairs.Add(repair);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRepairs), new { id = repair.Id }, repair);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepair(int id, Repair repair)
    {
        if (id != repair.Id) return BadRequest();

        _context.Entry(repair).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepair(int id)
    {
        var repair = await _context.Repairs.FindAsync(id);
        if (repair == null) return NotFound();

        _context.Repairs.Remove(repair);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


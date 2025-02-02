using WebApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Server.Api;

[Route("api/[controller]")]
[ApiController]
public class PartsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PartsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Part>>> GetParts()
    {
        return await _context.Parts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Part>> GetPart(int id)
    {
        var part = await _context.Parts.FindAsync(id);
        return part == null ? NotFound() : Ok(part);
    }

    [HttpPost]
    public async Task<ActionResult<Part>> AddPart(Part part)
    {
        if (part == null)
        {
            return BadRequest("Part data is required.");
        }

        if (string.IsNullOrWhiteSpace(part.Name))
        {
            return BadRequest("Part name is required.");
        }

        if (part.Price <= 0)
        {
            return BadRequest("Price must be greater than 0.");
        }

        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPart), new { id = part.Id }, part);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePart(int id, Part part)
    {
        if (id != part.Id)
        {
            return BadRequest("Part ID mismatch.");
        }

        var existingPart = await _context.Parts.FindAsync(id);
        if (existingPart == null)
        {
            return NotFound($"Part with ID {id} not found.");
        }

        existingPart.Name = part.Name;
        existingPart.Manufacturer = part.Manufacturer;
        existingPart.Price = part.Price;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, "Error updating part. Please try again.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePart(int id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null)
        {
            return NotFound($"Part with ID {id} not found.");
        }

        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

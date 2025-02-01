using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Part
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int RepairId { get; set; }
}

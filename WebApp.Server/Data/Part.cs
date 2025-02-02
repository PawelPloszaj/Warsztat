using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Part
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string Manufacturer { get; set; } = string.Empty;
    [Required, Range(0.01, double.MaxValue)] public decimal Price { get; set; }

    public int RepairId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Repair
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(255)] public string Description { get; set; } = string.Empty;
    [Required] public DateTime RepairDate { get; set; }
    [Required, Range(0.01, double.MaxValue)] public decimal Cost { get; set; }

    [Required] public int RepairOrderId { get; set; }

    public List<Part> Parts { get; set; } = new();
    public List<Mechanic> Mechanics { get; set; } = new();
}

using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Repair
{
    [Key] public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime RepairDate { get; set; }
    public decimal Cost { get; set; }

    public int RepairOrderId { get; set; }

    public List<Part> Parts { get; set; } = new();
    public List<Mechanic> Mechanics { get; set; } = new();
}

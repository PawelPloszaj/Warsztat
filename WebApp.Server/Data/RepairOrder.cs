using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class RepairOrder
{
    [Key] public int Id { get; set; }
    [Required] public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required] public string Status { get; set; } = string.Empty;

    [Required] public int VehicleId { get; set; }
    public List<Repair> Repairs { get; set; } = new();
}

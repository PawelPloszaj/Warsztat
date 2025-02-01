using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class RepairOrder
{
    [Key] public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public int VehicleId { get; set; }
    public List<Repair> Repairs { get; set; } = new();
}

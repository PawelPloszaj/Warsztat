namespace WebApp.Shared.Dto;

public class RepairOrderDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public int VehicleId { get; set; }

    public List<RepairDto> Repairs { get; set; } = new();
}


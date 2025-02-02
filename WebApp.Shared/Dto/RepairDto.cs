namespace WebApp.Shared.Dto;
using System.Text.Json.Serialization;
public class RepairDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime RepairDate { get; set; }
    public decimal Cost { get; set; }

    public int RepairOrderId { get; set; }

    public List<PartDto> Parts { get; set; } = new();
    [JsonIgnore] public List<MechanicDto> Mechanics { get; set; } = new();
}


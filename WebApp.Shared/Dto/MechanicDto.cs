namespace WebApp.Shared.Dto;
using System.Text.Json.Serialization;

public class MechanicDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [JsonIgnore] public List<RepairDto> Repairs { get; set; } = new();
}


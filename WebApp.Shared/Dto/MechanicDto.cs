namespace WebApp.Shared.Dto;

public class MechanicDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<RepairDto> Repairs { get; set; } = new();
}


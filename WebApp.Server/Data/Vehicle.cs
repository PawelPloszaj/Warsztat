using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Vehicle
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(15)] public string LicensePlate { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string Brand { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string Model { get; set; } = string.Empty;
    [Required] public int Year { get; set; }

    [Required] public int ClientId { get; set; }

    public List<RepairOrder> RepairOrders { get; set; } = new();
}

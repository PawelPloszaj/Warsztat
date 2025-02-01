using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Vehicle
{
    [Key] public int Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }

    public int ClientId { get; set; }

    public List<RepairOrder> RepairOrders { get; set; } = new();
}

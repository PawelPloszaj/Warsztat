using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Client
{
    [Key] public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public List<Vehicle> Vehicles { get; set; } = new();
}

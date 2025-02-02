using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Client
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string LastName { get; set; } = string.Empty;
    [Required, MaxLength(100)] public string Address { get; set; } = string.Empty;
    [Required, MaxLength(13)] public string Phone { get; set; } = string.Empty;

    public List<Vehicle> Vehicles { get; set; } = new();
}

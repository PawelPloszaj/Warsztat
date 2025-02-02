using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class Mechanic
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string LastName { get; set; } = string.Empty;

    public List<Repair> Repairs { get; set; } = new();
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Server.Data;

public class Mechanic
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string LastName { get; set; } = string.Empty;

    [JsonIgnore] public List<Repair> Repairs { get; set; } = new();
}

using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.Data;

public class User
{
    [Key] public int Id { get; set; }
    [MaxLength(32)] public string Username { get; set; } = String.Empty;
    [MaxLength(64)] public string Password { get; set; } = String.Empty;
    [MaxLength(320)] public string Email { get; set; } = String.Empty;
    [MaxLength(20)] public string Role { get; set; } = String.Empty;
}
﻿namespace WebApp.Shared.Dto;

public class PartDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int RepairId { get; set; }
}

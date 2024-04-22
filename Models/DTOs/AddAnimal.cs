﻿using System.ComponentModel.DataAnnotations;

namespace Tutorial5.Models.DTOs;

public class AddAnimal
{
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
    [MaxLength(200)]
    public string Category { get; set; }
    [MaxLength(200)]
    public string Area { get; set; }

    public AddAnimal(string name, string? description, string category, string area)
    {
        Name = name;
        Description = description;
        Category = category;
        Area = area;
    }
}
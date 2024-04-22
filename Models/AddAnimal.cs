using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Tutorial5.Models.DTOs;

public class AddAnimal
{
   
    [MinLength(3)]
    [MaxLength(200)]
    [Required]    
    public string Name { get; set; }
    [MaxLength(200)]
    
    public string? Description { get; set; }
    [MaxLength(200)]  
    [Required]    
    public string Category { get; set; }
    [Required]    
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
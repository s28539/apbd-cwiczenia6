using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;
using Tutorial5.Repositories;

namespace Tutorial5.Controllers;

[ApiController]
// [Route("api/animals")]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy = null)
    {
        var animals = PostgreSQLRepository
            .GetRepository(_configuration.GetConnectionString("Default"))
            .AsEnumerable();
        
        if (!string.IsNullOrEmpty(orderBy))
        {
            switch (orderBy)
            {
                case "Name":
                    animals = animals.OrderBy(a => a.Name);
                    break;
                case "Description":
                    animals = animals.OrderBy(a => a.Desription);
                    break;
                case "Category":
                    animals = animals.OrderBy(a => a.Category);
                    break;
                case "Area":
                    animals = animals.OrderBy(a => a.Area);
                    break;
                default:
                    return BadRequest("Invalid input OrderBy sorry:/");
            }
        }
        else
        {
             animals = animals.OrderBy(a => a.Name);
        }

        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        PostgreSQLRepository.AddAnimal(_configuration.GetConnectionString("Default"),addAnimal);
        
        return Created("", null);
    }
}
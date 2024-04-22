using Npgsql;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public class PostgreSQLRepository : Repository
{
    public static  List<Animal> GetRepository(string connectionString)
    {
        List<Animal> animals = new List<Animal>();
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using NpgsqlCommand command = new NpgsqlCommand();
        command.Connection = conn;
        command.CommandText = "SELECT * FROM Animal";
        var reader = command.ExecuteReader();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        int descriptionOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
                Desription = reader.GetString(descriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
        }
        
        return animals;
    }

    public static void AddAnimal(string connectionString, AddAnimal addAnimal)
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using NpgsqlCommand command = new NpgsqlCommand();
        command.Connection = conn;
        command.Parameters.AddWithValue("@animalName", addAnimal.Name);
        command.Parameters.AddWithValue("@animalDescription", addAnimal.Description);
        command.Parameters.AddWithValue("@animalCategory", addAnimal.Category);
        command.Parameters.AddWithValue("@animalArea", addAnimal.Area);
        command.CommandText = 
            "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@animalName, @animalDescription,@animalCategory ,@animalArea)";
        command.ExecuteNonQuery();
        
    }
}
using Microsoft.AspNetCore.Http.HttpResults;
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

    public static void AddAnimal(string connectionString,AddAnimal addAnimal)
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

    public static void UpdateAnimal(string connectionString, int idAnimal, UpdateAnimal updateAnimal)
    { 
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using NpgsqlCommand command = new NpgsqlCommand();
        command.Connection = conn;
        command.Parameters.AddWithValue("@animalId", idAnimal);
        command.Parameters.AddWithValue("@animalName", updateAnimal.Name);
        command.Parameters.AddWithValue("@animalDescription", updateAnimal.Description);
        command.Parameters.AddWithValue("@animalCategory", updateAnimal.Category);
        command.Parameters.AddWithValue("@animalArea", updateAnimal.Area);
        // UPDATE table_name
        // SET column1 = value1, column2 = value2, ...
        // WHERE condition;
        command.CommandText = 
            "UPDATE animal SET " +
            "Name = @animalName, Description= @animalDescription, Category= @animalCategory , Area = @animalArea  " +
            "where IdAnimal = @animalId";
        command.ExecuteNonQuery();
    }

    public static void DeleteAnimal(string connectionString, int idAnimal)
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using NpgsqlCommand command = new NpgsqlCommand();
        command.Connection = conn;
        command.Parameters.AddWithValue("@animalId", idAnimal);
        command.CommandText = "DELETE from animal where IdAnimal = @animalId";
        command.ExecuteNonQuery();
        
    }
}
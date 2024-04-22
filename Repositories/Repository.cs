using Npgsql;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public interface Repository
{
    public  List<Animal> GetRepository(string connectionString);

    public  void AddAnimal(string connectionString, AddAnimal addAnimal);

    public  void UpdateAnimal(string connectionString, int idAnimal, UpdateAnimal updateAnimal);

    public void DeleteAnimal(string connectionString, int idAnimal);

}
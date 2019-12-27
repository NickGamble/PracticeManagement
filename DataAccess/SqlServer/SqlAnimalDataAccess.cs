using Common.DataAccess;
using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess.SqlServer
{
    public class SqlAnimalDataAccess : IAnimalDataAccess
    {
        private string _cxString = "Server =.\\SqlExpress; Database=VetDb; Trusted_connection=true;"; // TODO: Obtain from config

        public void AddAnimal(AnimalDto animalDto)
        {
            // TODO: Use stored procs or ORM (e.g. entityframework)
            var insertQuery = @"INSERT INTO Animal (Id, MicrochipId, Name, DateOfBirth, Breed, Species, Colour)
                VALUES (@Id, @MicrochipId, @Name, @DateOfBirth, @Breed, @Species, @Colour)";


            using (var connection = new SqlConnection(_cxString))
            {
                connection.Open();
                var command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("Id", animalDto.Id);
                command.Parameters.AddWithValue("MicrochipId", animalDto.MicrochipId);
                command.Parameters.AddWithValue("Name", animalDto.Name);
                command.Parameters.AddWithValue("DateOfBirth", animalDto.DateOfBirth);
                command.Parameters.AddWithValue("Breed", animalDto.Breed);
                command.Parameters.AddWithValue("Species", animalDto.Species);
                command.Parameters.AddWithValue("Colour", animalDto.Colour);

                command.ExecuteNonQuery();
            }
        }

        public List<AnimalDto> GetAllAnimals()
        {            
            var selectQuery = @"SELECT Id, MicrochipId, Name, DateOfBirth, Breed, Species, Colour FROM Animal";

            using (var connection = new SqlConnection(_cxString))
            {
                connection.Open();
                using (var command = new SqlCommand(selectQuery, connection))
                {
                    return ReadResults(command);                   
                }
            }            
        }

        public List<AnimalDto> SearchAnimals(string searchString)
        {
            var selectQuery = @"SELECT Id, MicrochipId, Name, DateOfBirth, Breed, Species, Colour FROM Animal
                                WHERE MicrochipId LIKE @searchString
                                OR Name LIKE @searchString
                                OR DateOfBirth LIKE @searchString
                                OR Breed LIKE @searchString
                                OR Species LIKE @searchString
                                OR Colour LIKE @searchString";

            using (var connection = new SqlConnection(_cxString))
            {
                connection.Open();
                using (var command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("searchString", "%" + searchString + "%");
                    return ReadResults(command);
                }
            }
        }

        private List<AnimalDto> ReadResults(SqlCommand command)
        {
            var results = new List<AnimalDto>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new AnimalDto
                {
                    Id = Guid.Parse(reader["Id"].ToString()),
                    MicrochipId = reader["MicrochipId"].ToString(),
                    Name = reader["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    Breed = reader["Breed"].ToString(),
                    Species = reader["Species"].ToString(),
                    Colour = reader["Colour"].ToString()
                });
            }
            return results;
        }
    }
}

using Common.Dtos;
using System;
using System.Collections.Generic;

namespace Common.DataAccess
{
    public interface IAnimalDataAccess
    {
        List<AnimalDto> GetAllAnimals();
        void AddAnimal(AnimalDto animalDto);
        List<AnimalDto> SearchAnimals(string searchString);
    }
}

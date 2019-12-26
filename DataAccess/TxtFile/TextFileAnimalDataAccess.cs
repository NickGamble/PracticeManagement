using Common.DataAccess;
using Common.Dtos;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess.TxtFile
{
    public class TextFileAnimalDataAccess : IAnimalDataAccess
    {
        private string _path = "PracticeManagement_AnimalTable.txt";
        private const int MicroChipColumnIndex = 1;
        public TextFileAnimalDataAccess()
        {
            if (!File.Exists(_path))
                File.CreateText(_path);
        }

        public void AddAnimal(AnimalDto animalDto)
        {
            var microChipId = animalDto.MicrochipId;
            if (MicrochipIdExists(microChipId))
                throw new DuplicateMicrochipIdException($"Microchip Id '{microChipId}' already exists");

            using (var sw = File.AppendText(_path))
            {
                sw.Write($@"{animalDto.ToCsv}{Environment.NewLine}");
            }
        }
        
        private bool MicrochipIdExists(string microchipId)
        {
            var fileText = File.ReadAllText(_path);
            var animalCsvs = fileText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var microChipIds = animalCsvs.Select(x => x.Split(',')).Select(x => x[MicroChipColumnIndex]);
            return microChipIds.Contains(microchipId);

        }

        public List<AnimalDto> GetAllAnimals()
        {
            var fileText = File.ReadAllText(_path);
            var animalCsvs = fileText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return animalCsvs.Select(x => AnimalDto.FromCsv(x)).ToList();
        }

        public List<AnimalDto> SearchAnimals(string searchString)
        {
            var fileText = File.ReadAllText(_path);
            var animalCsvs = fileText.Split(Environment.NewLine);
            var results = animalCsvs.Where(x => x.Contains(searchString));
            return results.Select(x => AnimalDto.FromCsv(x)).ToList();
        }
    }
}

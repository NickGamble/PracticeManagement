using Common.DataAccess;
using Common.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.TxtFile
{
    public class TextFileAnimalDataAccess : IAnimalDataAccess
    {
        private string _path = "PracticeManagement_AnimalTable.txt";
        public TextFileAnimalDataAccess()
        {
            if (!File.Exists(_path))
                File.CreateText(_path);
        }

        public void AddAnimal(AnimalDto animalDto)
        {
            using (var sw = File.AppendText(_path))
            {
                sw.Write($@"{animalDto.ToCsv}{Environment.NewLine}");
            }
        }

        public List<AnimalDto> GetAllAnimals()
        {
            var fileText = File.ReadAllText(_path);
            var animalCsvs = fileText.Split(Environment.NewLine);
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

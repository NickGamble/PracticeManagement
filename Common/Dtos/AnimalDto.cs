using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class AnimalDto
    {
        public Guid Id { get; set; }
        public string MicrochipId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; }
        public string Colour { get; set; }

        public string ToCsv => @$"{Id},{MicrochipId},{Name},{DateOfBirth.ToShortDateString()},{Breed},{Species},{Colour}";

        public static AnimalDto FromCsv  (string csvString)
        {
            try
            {
                var splitString = csvString.Split(',');
                return new AnimalDto
                {
                    Id = Guid.Parse(splitString[0]),
                    MicrochipId = splitString[1],
                    Name = splitString[2],
                    DateOfBirth = DateTime.Parse(splitString[3]),
                    Breed = splitString[4],
                    Species = splitString[5],
                    Colour = splitString[6]
                };
            }
            catch(Exception ex)
            {
                throw new Exception("Error converting animal from CSV", ex);
            }
        }
    }
}

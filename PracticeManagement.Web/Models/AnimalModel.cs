using Common.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace PracticeManagement.Web.Models
{
    public class AnimalModel
    {
        public AnimalModel()
        {

        }
        public AnimalModel(AnimalDto dto)
        {
            Id = dto.Id;
            MicrochipId = dto.MicrochipId;
            Name = dto.Name;
            DateOfBirth = dto.DateOfBirth;
            Breed = dto.Breed;
            Species = dto.Species;
            Colour = dto.Colour;
        }
    
        public Guid Id { get; set; }
        public string MicrochipId { get; set; }        
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; }
        public string Colour { get; set; }

        public AnimalDto ToDto()
        {
            return new AnimalDto
            {
                Id = Id,
                MicrochipId = MicrochipId,
                Name = Name,
                DateOfBirth = DateOfBirth,
                Breed = Breed,
                Species = Species,
                Colour = Colour
            };
        }
    }
}

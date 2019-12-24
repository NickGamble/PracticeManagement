using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DataAccess;
using DataAccess.TxtFile;
using Microsoft.AspNetCore.Mvc;
using PracticeManagement.Web.Models;

namespace PracticeManagement.Web.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalDataAccess _animalDataAccess;
        public AnimalController()
        {
            _animalDataAccess = new TextFileAnimalDataAccess(); // TODO: Use IoC container to determine concrete instance of IAnimalDataAccess
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewAll()
        {
            var model = _animalDataAccess.GetAllAnimals().Select(x => new AnimalModel(x));
            return View(model);
        }

        public IActionResult Search()
        {
            // var model = _animalDataAccess.SearchAnimals();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalModel model)
        {
            model.Id = Guid.NewGuid();
            _animalDataAccess.AddAnimal(model.ToDto());

            return View(model);
        }
    }
}
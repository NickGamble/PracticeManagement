using System;
using System.Collections.Generic;
using System.Linq;
using Common.DataAccess;
using Common.Exceptions;
using DataAccess.SqlServer;
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
            //_animalDataAccess = new SqlAnimalDataAccess();
        }

        public IActionResult ViewAll(string searchString)
        {
            IEnumerable<AnimalModel> model;
            if (string.IsNullOrEmpty(searchString))
            {
                model = _animalDataAccess.GetAllAnimals().Select(x => new AnimalModel(x));
            }
            else
            {
                model = _animalDataAccess.SearchAnimals(searchString).Select(x => new AnimalModel(x));                
            }
            return View(model);
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

            try
            {
                _animalDataAccess.AddAnimal(model.ToDto());
            }
            catch(DuplicateMicrochipIdException ex)
            {
                ModelState.AddModelError("MicrochipId", ex.Message);
            }
            return RedirectToAction("ViewAll");
        }

        public IActionResult Details(AnimalModel model)
        {
            return View(model);
        }
    }
}
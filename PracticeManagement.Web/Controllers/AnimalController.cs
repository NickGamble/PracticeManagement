﻿using System;
using System.Linq;
using Common.DataAccess;
using Common.Exceptions;
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
﻿using GameInventoryManager.Data;
using GameInventoryManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace GameInventoryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var gamesList = _db.games.ToList();
            return View(gamesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Games games)
        {
            if(games.name == games.price.ToString())
            {
                ModelState.AddModelError("name", "The game cannot have the same name as the price.");
            }

            // Vérification des doublons
            var existingGame = _db.games.FirstOrDefault(g => g.name == games.name);
            if(existingGame != null)
            {
                ModelState.AddModelError("name", "This game name already exists.");
            }

            if(ModelState.IsValid)
            {
                // Vérification d'un prix possitive
                if (games.price >= 0)
                {
                    _db.games.Add(games);
                    _db.SaveChanges();
                    TempData["success"] = "Your game has been added successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("name", "The price you entered is not valid.");
                }
                    
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Games? gamesdb = _db.games.FirstOrDefault(u => u.id == id);
            if (gamesdb == null)
            {
                return NotFound();
            }
            return View(gamesdb);
        }

        [HttpPost]
        public IActionResult Edit(Games games)
        {
            if (games.name == games.price.ToString())
            {
                ModelState.AddModelError("name", "The game cannot have the same name as the price.");
            }

            // Vérification des doublons
            var existingGame = _db.games.FirstOrDefault(g => g.name == games.name);
            if (existingGame != null)
            {
                ModelState.AddModelError("name", "This game name already exists.");
            }

            if (ModelState.IsValid)
            {
                // Vérification d'un prix possitive
                if (games.price >= 0)
                {
                    _db.games.Update(games);
                    _db.SaveChanges();
                    TempData["success"] = "Your game has been update successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("name", "The price you entered is not valid.");
                }

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var gamesdb = _db.games.Find(id);
            if (gamesdb == null)
            {
                return NotFound();
            }
            _db.games.Remove(gamesdb);
            _db.SaveChanges();
            TempData["success"] = "The game was successfully deleted.";
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
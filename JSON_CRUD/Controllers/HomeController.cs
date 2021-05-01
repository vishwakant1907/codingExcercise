using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JSON_CRUD.Models;
using JSON_CRUD.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JSON_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataInventory _dataInventory;
        public HomeController(IDataInventory dataInventory)
        {
            // Implemeted Dependency Injection 
            _dataInventory = dataInventory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var inventory = _dataInventory.GetInventory();
            if(inventory==null)
            {
                return View(null);
            }
            return View(inventory);
        }

        [HttpPost]
        public IActionResult Index(Inventory Model)
        {
            var data = _dataInventory.AddUpdateInventory(Model);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dataInventory.DeleteInventoryById(id);

            return RedirectToAction("index", "Home");
        }
    }

   
}

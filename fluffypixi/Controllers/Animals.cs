using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fluffypixi.Controllers
{
    public class Animals : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Animals/getMy")]
        public List<Models.Repository.ModAnimal> getMy(string email)
        {
           
            return Models.Repository.Animals.GetAnimals(email); 
        }
        [HttpPost("Animals/add")]
        public bool add(string name, string date, string type, string gender, string klichka,string email)
        {
            Models.Repository.Animals animal = new Models.Repository.Animals(name, date, type, gender, klichka, email);
            return true;
        }
        [HttpGet("Animals/get")]
        public Models.Repository.Animals add(int id)
        {
            Models.Repository.Animals animal = new Models.Repository.Animals(id);
            return animal; 
        }
    }
}

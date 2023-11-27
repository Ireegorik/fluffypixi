using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fluffypixi.Controllers
{
    public class Profile : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Profile/register")]
        public bool register(string password, string email, string invateCode)
        {
            if (Models.Repository.CheckInvite(invateCode))
            {
                Models.Repository.Profile profile = new Models.Repository.Profile(password, email, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost("Profile/auth")]
        public string auth(string email, string password)
        {
            Models.Repository.Profile profile = new Models.Repository.Profile(password, email, 1);
            if(profile.Email != null)
            {
                return "OK";
            }
            else
            {
                return "NO";
            }
        }
        [HttpPost("Profile/Update")]
        public string update(string Email, string FIO, string TEL, string Adress)
        { 
            if (Models.Repository.Profile.UpdateInfo(Email, FIO, TEL, Adress))
            {
                return "OK";
            }
            else
            {
                return "NO";
            }
        }
    }
}


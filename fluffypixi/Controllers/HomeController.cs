using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fluffypixi.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace fluffypixi.Controllers
{
    public class HomeController : Controller
    {
        private object _appEnvironment;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
        [HttpGet]
        [ActionName("GenerateInvite")]
        public string GenerateInvite()
        {

            return Models.Repository.GenerateInvite();
        }
        [HttpPost("Home/AddFile")]
        public bool AddFile(IFormFile uploadedFile,string whosend)
        {
            if (uploadedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    uploadedFile.CopyTo(memoryStream);
                    
                    return Models.Repository.Profile.AddPostImage(memoryStream.ToArray(), whosend) ;
                }
            }
            else
            {
                return false;
            }

            
        }
        [HttpPost("Home/GetPosts")]
        public bool GetPosts(IFormFile uploadedFile, string whosend)
        {
            if (uploadedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    uploadedFile.CopyTo(memoryStream);

                    return Models.Repository.Profile.AddPostImage(memoryStream.ToArray(), whosend);
                }
            }
            else
            {
                return false;
            }


        }
    }
}

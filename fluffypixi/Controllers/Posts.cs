using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fluffypixi.Controllers
{
    public class Posts : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Posts/NewFAQ")]
        public bool NewFAQ(string email, string message, string time)
        {
            Models.Repository.createFAQ(email, message, time, 0, -1);
            return true;
        }
        [HttpPost("Posts/AnswerFAQ")]
        public bool AnswerFAQ(string email, string message, string time, int branchID)
        {
            Models.Repository.createFAQ(email, message, time, 1, branchID);
            return true;
        }
        [HttpGet("Posts/getMyFAQsBranchs")]
        public List<Models.Repository.FAQ_Model> getMyFAQsBranchs(string email)
        {
            return Models.Repository.getMyFAQsBranchs(email);
        }
        [HttpGet("Posts/getFAQs")]
        public List<Models.Repository.FAQ_Model> getFAQs()
        {
            return Models.Repository.getFAQs();
        }
        [HttpGet("Posts/getFAQ")]
        public List<Models.Repository.FAQ_Model> getFAQ(int branchID)
        {
            return Models.Repository.getFAQ(branchID);
        }


    }
}

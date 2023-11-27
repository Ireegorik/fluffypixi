using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static fluffypixi.Models.Repository;
namespace fluffypixi.Views.Home
{
    public class AdminModel : PageModel
    {
        public string code = " ";
        public void OnGet()
        {
            
        }
        public void Btn_Click()
        {
           code = GenerateInvite();
        }

    }
}
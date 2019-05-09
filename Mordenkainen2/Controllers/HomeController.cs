using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mordenkainen2.Models;


namespace Mordenkainen2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CharacterCreation()
        {
            ViewData["Message"] = "Create a Character!";

            return View();
        }

        public IActionResult CharacterSheet()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //helps prevent cross scripting
        [ValidateAntiForgeryToken]
        //specifies parameter is coming from the body?
        public IActionResult Register([FromBody]LayoutViewModel user)
        {
            // boolean variable results takes the return value of RegisterUser, which trys to add a user
            //and return true, or if the adding fails it catches and returns false;
            bool result = EFQueries.RegisterUser(user.RegisterModel);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        //have to pass in the matching ViewModel(class) used in the form.
        public IActionResult Login(LayoutViewModel login)
        {
            //resutl takes a nullable int. LoginUser takes the login inputs and checks them against the database
            //return either a userID or a null
            int? result = EFQueries.LoginUser(login.LoginModel);
            if (result != null)
            {
                //if not null addes userID to the session
                HttpContext.Session.SetInt32("_UserID", (int)result);
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}

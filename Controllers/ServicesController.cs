using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using qaea.Models;
using System.Linq;

namespace qaea.Controllers
{
    public class ServicesController : Controller
    {
        private QaeaContext _context;
 
        public ServicesController(QaeaContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("services")]
        public IActionResult Services(){
            ViewBag.loggedIn = LoggedIn();
            if(LoggedIn() == true){
                ViewBag.currentUser = GetUser();
            }
            return View("Services");
        }

        public bool LoggedIn(){
            if(HttpContext.Session.GetInt32("userID") != null){
                return true;
            }
            else{
                return false;
            }
        }
        public User GetUser(){
            User currentUser = _context.Users.Single(u => u.UserID == HttpContext.Session.GetInt32("userID"));
            return currentUser;
        }
    }
}
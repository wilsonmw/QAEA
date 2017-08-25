using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using qaea.Models;
using System.Linq;

namespace Auctions.Controllers
{
    public class RegController : Controller
    {
        private QaeaContext _context;
 
        public RegController(QaeaContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("login")]
        public IActionResult Index()
        {
            ViewBag.loggedIn = LoggedIn();
            if(LoggedIn() == true){
                ViewBag.currentUser = GetUser();
            }
            ViewBag.existsError = HttpContext.Session.GetString("existsError");
            ViewBag.loginError = HttpContext.Session.GetString("loginError");
            HttpContext.Session.Clear();
            return View("loginReg");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model){
            if(!ModelState.IsValid){
                return View("loginReg");
            }
            else{
                bool exists = _context.Users.Any(u => u.Email == model.Email);
                if (exists == true){
                    HttpContext.Session.SetString("existsError", "That email address is already in use, please try again.");
                    return RedirectToAction ("Index");
                }
                else
                {
                    User newUser = new User{
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                    };
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    User currentUser = _context.Users.Single(u => u.Email == newUser.Email);
                    HttpContext.Session.SetInt32("userID", currentUser.UserID);
                    HttpContext.Session.SetString("loggedIn", "yes");
                    return RedirectToAction("Home", "Home");
                } 
            }
        }

        // [HttpPost]
        // [Route("addAddress")]
        // public IActionResult AddAddress(){
        //     ViewBag.loggedIn = LoggedIn();
        //     if(LoggedIn() == true){
        //         ViewBag.currentUser = GetUser();
        //     }
        //     if(LoggedIn() == false){
        //         HttpContext.Session.SetString("loginError", "You must be logged in to schedule or view appointments.");
        //         return RedirectToAction("Index", "Reg");
        //     }
        //     return View("AddAddress");
        // }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password){
            if (Email == null || Password == null){
                HttpContext.Session.SetString("loginError", "Login failed, please try again.");
                return RedirectToAction("Index");
            }
            else{
                bool exists = _context.Users.Any(u => u.Email == Email);
                if(exists == true){
                    User currentUser = _context.Users.Single(u => u.Email == Email);
                    if (currentUser.Password == Password){
                        HttpContext.Session.SetInt32("userID", currentUser.UserID);
                        return RedirectToAction("Home", "Home");
                    }
                    else{
                        HttpContext.Session.SetString("loginError", "Login failed, please try again.");
                        return RedirectToAction("Index");
                    }
                }
                else{
                    HttpContext.Session.SetString("loginError", "Login failed, please try again.");
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Home");
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
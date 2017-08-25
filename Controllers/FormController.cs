using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using qaea.Models;
using System.Linq;

namespace qaea.Controllers
{
    public class FormController : Controller
    {
        [HttpGet]
        [Route("forms")]
        public IActionResult Forms(){
            return View("Forms");
        }
    }
}
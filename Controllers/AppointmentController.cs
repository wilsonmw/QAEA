using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using qaea.Models;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Requests;

using System.Text;
using System.Threading;


namespace qaea.Controllers
{
    public class AppointmentController : Controller
    {
        private QaeaContext _context;
        static string[] Scopes = { CalendarService.Scope.Calendar};
        static string ApplicationName = "QAEA";
        public AppointmentController(QaeaContext context)
        {
            _context = context;
        }
            
        [HttpGet]
        [Route("appointmentsSelect")]
        public IActionResult AppointmentsSelect(){
            ViewBag.loggedIn = LoggedIn();
            if(LoggedIn() == true){
                ViewBag.currentUser = GetUser();
            }
            if(LoggedIn() == false){
                HttpContext.Session.SetString("loginError", "You must be logged in to schedule or view appointments.");
                return RedirectToAction("Index", "Reg");
            }
            return View("AppointmentsSelect");
        }

        
        [HttpGet]
        [Route("appointments")]
        public IActionResult Appointments(){
            ViewBag.loggedIn = LoggedIn();
            if(LoggedIn() == true){
                ViewBag.currentUser = GetUser();
            }
            if(LoggedIn() == false){
                HttpContext.Session.SetString("loginError", "You must be logged in to schedule or view appointments.");
                return RedirectToAction("Index", "Reg");
            }
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = @"/Users/Monica/Desktop/DojoAssignments/C#/qaea";
                credPath = Path.Combine(credPath, ".credentials/client_secret.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            { 
                    ViewBag.upcoming = events;
            }
            else
            {
                ViewBag.upcomingError = "No upcoming events found.";
            }
            ViewBag.newdate = HttpContext.Session.GetString("newDate");
            ViewBag.newAppointment = HttpContext.Session.GetString("newAppointment");
            return View("Appointments");
        }

        [HttpGet]
        [Route("newAppointment")]
        public IActionResult NewAppointment(){
            ViewBag.loggedIn = LoggedIn();
            if(LoggedIn() == true){
                ViewBag.currentUser = GetUser();
            }
            if(LoggedIn() == false){
                HttpContext.Session.SetString("loginError", "You must be logged in to schedule or view appointments.");
                return RedirectToAction("Index", "Reg");
            }
            
            return View("NewAppointment");
        }

        [HttpPost]
        [Route("createAppointment")]
        public  IActionResult Create(string date, string reason){
            // Event newEvent = new Event()
            // {
            //     Summary = reason,
            //     Location = "1245 Queen Anne Ave, Seattle, WA 98109",
            //     Description = "QAEA Services",
            //     Start = new EventDateTime()
            //     {
            //         DateTime = (date),
            //         TimeZone = "America/Los_Angeles",
            //     },
            //     End = new EventDateTime()
            //     {
            //         DateTime = (date),
            //         TimeZone = "America/Los_Angeles",
            //     },
            //     Recurrence = new String[] { "RRULE:FREQ=ONCE;COUNT=1" },
            //     Attendees = new EventAttendee[] {
            //         new EventAttendee() { Email = "matthewwarrenwilson@gmail.com" },
                    
            //     },
            //     Reminders = new Event.RemindersData()
            //     {
            //         UseDefault = false,
            //         Overrides = new EventReminder[] {
            //             new EventReminder() { Method = "email", Minutes = 24 * 60 },
            //             new EventReminder() { Method = "sms", Minutes = 10 },
            //         }
            //     }
            // };
            // UserCredential credential;

            // using (var stream =
            //     new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            // {
            //     string credPath = @"/Users/Monica/Desktop/DojoAssignments/C#/qaea";
            //     credPath = Path.Combine(credPath, ".credentials/client_secret.json");

            //     credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //         GoogleClientSecrets.Load(stream).Secrets,
            //         Scopes,
            //         "user",
            //         CancellationToken.None,
            //         new FileDataStore(credPath, true)).Result;
            // }
            // var service = new CalendarService(new BaseClientService.Initializer()
            //     {
            //         HttpClientInitializer = credential,
            //         ApplicationName = ApplicationName,
            //     });

            // String calendarId = "primary";
            // EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            // Event createdEvent = request.Execute();
            // return RedirectToAction("Appointments");
            HttpContext.Session.SetString("newDate", date);
            HttpContext.Session.SetString("newAppointment", reason);
            return RedirectToAction("Appointments");
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
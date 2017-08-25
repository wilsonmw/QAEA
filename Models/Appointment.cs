using qaea.Models;
using System.Linq;
using System;

namespace qaea.Models
{
    public class Appointment
    {
        public int AppointmentID {get; set;}
        public DateTime Date {get; set;}
        public DateTime Time {get; set;}
        public decimal Duration {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public int UserID {get; set;}
        public User User {get; set;}
        public int ChildID {get; set;}
        public Child Child {get; set;}
    }
}
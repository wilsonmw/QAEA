using qaea.Models;
using System.Linq;
using System.Collections.Generic;

public class User
{
    public int UserID {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public List<Children_has_Users> Kids {get; set;}
    public List<Address> Addresses {get; set;}
    public List<Appointment> Appointments {get; set;}


    public User(){
        Kids = new List<Children_has_Users>();
        Addresses = new List<Address>();
        Appointments = new List<Appointment>();
    }
}
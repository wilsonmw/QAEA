using qaea.Models;
using System.Linq;
using System;

public class Address
{
    public int AddressID {get; set;}
    public string Address1 {get; set;}
    public string Address2 {get; set;}
    public string City {get; set;}
    public string State {get; set;}
    public int ZipCode {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public int UserID {get; set;}
    public User User {get; set;}

}
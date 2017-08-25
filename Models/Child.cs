using qaea.Models;
using System.Linq;
using System;

public class Child
{
    public int ChildID {get; set;}
    public string ChildFirstName {get; set;}
    public string ChildLastName {get; set;}
    public int Age {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public int UserID {get; set;}
    public User PrimaryParent {get; set;}
}
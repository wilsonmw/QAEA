using qaea.Models;
using System.Linq;

public class Children_has_Users
{
    public int Children_has_UsersID {get; set;}
    public int ChildID {get; set;}
    public Child Child {get; set;}
    public int UserID {get; set;}
    public User User {get; set;}

    public Children_has_Users()
    {
        Child = Child;
        User = User;
    }
}

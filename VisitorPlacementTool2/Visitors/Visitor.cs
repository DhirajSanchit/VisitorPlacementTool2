using System;

namespace VisitorPlacementTool2.Visitors;

public class Visitor
{
    public string Name { get; private set; }
 
    public DateTime DateOfBirth { get; private set; }
    public DateTime RegisteredTime { get; private set; }
    public int GroupId { get; set; }
    public string Rejection {get; set;}

    public Visitor(string name, DateTime dateOfBirth, DateTime registeredTime)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        RegisteredTime = registeredTime;
    }

    public bool IsAnAdult(DateTime eventDate)
    {
        switch (eventDate.Year - DateOfBirth.Year)
        {
            case > 12:
                return true;
            case 12:
                switch (eventDate.Month - DateOfBirth.Month)
                {
                    case > 0:
                        return true;
                    case 0:
                        return (eventDate.Day >= DateOfBirth.Day);
                    default:
                        return false;
                }
            default:
                return false;
        }
    }
    
    
}
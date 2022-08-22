using System;

namespace VisitorPlacementTool2.Visitors;
/// <summary>
/// Representation of visitors in the simulation.
/// </summary>
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
    
    //Calculate the age of the visitor which is needed to allow or reject visitors based on age if they're
    //over 18 and accompanied by a parent / adults
    public bool IsAnAdult(DateTime eventDate)
    {
        //Calculate the age of the visitor
        switch (eventDate.Year - DateOfBirth.Year)
        {
            //If the visitor is over 18,
            case > 12:
                return true;
            //12 years old 
            case 12:
                //Now check if the age is indeed correct on based on the event date
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
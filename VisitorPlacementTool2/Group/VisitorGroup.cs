using System;
using System.Collections.Generic;
using VisitorPlacementTool2.Visitors;

namespace VisitorPlacementTool2.Group;

//Represents the group of visitors
public class VisitorGroup
{
    private List<Visitor> Visitors;
    private int Id; 
    
    //Constructor
    public VisitorGroup(List<Visitor> visitors)
    {
        Visitors = visitors;
    }
    
    //Returns the list of visitors in the group
    public IReadOnlyCollection<Visitor> GetVisitors()
    {
        return Visitors;
    }
    
    //Returns the id of the group
    public int GetId()
    {
        return Id;
    }
        
    //Sets the id of the group
    public void SetId(int id)
    {
        Id = id;
    }

    //Calculates the total number of kids in the group based on the competition date
    public int AmountOfKids(DateTime competitionDate)
    {
        int amountOfKids = 0;
        foreach (Visitor visitor in Visitors)
        {
            if (!visitor.IsAnAdult(competitionDate))
            {
                amountOfKids++;
            }
        }
        return amountOfKids;
    }
    
    //Removes a visitor from this group
    public void RemoveVisitor(Visitor visitor)
    {
        Visitors.Remove(visitor);
    }

    public bool HasAdults(DateTime eventDate)
    {
        foreach (var visitor in Visitors)
        {
            if (visitor.IsAnAdult(eventDate))
            {
                return true;
            }
        }
        return false;
    }
}
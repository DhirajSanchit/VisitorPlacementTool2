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
    
    
    //Removes a visitor from this group
    public void RemoveVisitor(Visitor visitor)
    {
        Visitors.Remove(visitor);
    }

}